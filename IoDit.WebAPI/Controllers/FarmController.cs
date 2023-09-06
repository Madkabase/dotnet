using System.Security.Claims;
using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FarmController : ControllerBase, IBaseController
{
    private readonly IFarmService _farmService;
    private readonly IUserService _userService;
    private readonly IFarmUserService _farmUserService;
    private readonly IFieldService _fieldService;
    private readonly IThresholdPresetService _thresholdPresetService;

    public FarmController(
        IFarmService farmService,
        IUserService userService,
        IFarmUserService farmUserService,
        IFieldService fieldService,
        IThresholdPresetService thresholdPresetService
    )
    {
        _farmService = farmService;
        _userService = userService;
        _farmUserService = farmUserService;
        _fieldService = fieldService;
        _thresholdPresetService = thresholdPresetService;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<BO.UserBo> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = (userIdClaim?.Value)
            ?? throw new UnauthorizedAccessException("Invalid user");
        var user = await _userService.GetUserByEmail(userId);
        return user;
    }

    [HttpGet("myFarms")]
    public async Task<ActionResult<List<FarmDto>>> GetMyFarms()
    {
        var user = await GetRequestDetails();

        var farms = await _farmService.getUserFarms(user);
        return Ok(farms.Select(f =>
        new FarmDto
        {
            Id = f.Id,
            Name = f.Name,
            AppId = f.AppId,
            AppName = f.AppName,
            MaxDevices = f.MaxDevices,
            Owner = UserDto.FromBo(f.Owner)
        }).ToList());
    }

    [HttpGet("details/{farmId}")]
    public async Task<ActionResult<FarmDto>> GetFarmDetails([FromRoute] int farmId)
    {
        var user = await GetRequestDetails();

        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);

        // check if user not null or farm admin or app admin

        if (userFarm == null && userFarm?.FarmRole != FarmRoles.Admin && user.AppRole != AppRoles.AppAdmin)
        {
            throw new UnauthorizedAccessException("User does not have access to this farm");
        }

        var farm = await _farmService.getFarmDetailsById(farmId);
        FarmDto farmDto = new()
        {
            isRequesterAdmin = userFarm?.FarmRole == FarmRoles.Admin, //? useless, because app Admin won't change the field ?  || user.AppRole == AppRoles.AppAdmin
            Id = farm.Id,
            Name = farm.Name,
            AppId = farm.AppId,
            AppName = farm.AppName,
            MaxDevices = farm.MaxDevices,
            Owner = UserDto.FromBo(farm.Owner),

            Fields = farm.Fields?.Select(f =>
                new FieldDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Geofence = f.Geofence,
                    Threshold = ThresholdDto.FromBo(f.Threshold),
                    OverallMoistureLevel = _fieldService.CalculateOverAllMoistureLevel(f.Devices.ToList(), f.Threshold)
                }
        ).ToList()
        };
        farmDto.Users = farmDto.isRequesterAdmin ? (await _farmUserService.GetFarmUsers(farm)).Select(fu =>
        new FarmUserDto
        {
            User = UserDto.FromBo(fu.User),
            Farm = new FarmDto()
        }).ToList() : null;
        return Ok(farmDto);
    }

    [HttpPut("{farmId}/addFarmer")]
    public async Task<ActionResult> AddFarmer([FromRoute] int farmId, [FromBody] AddFarmerDTO addFarmerDTO)
    {
        var user = await GetRequestDetails();

        // check if user is farm admin
        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);
        if (userFarm == null || userFarm.FarmRole != FarmRoles.Admin)
        {
            throw new UnauthorizedAccessException("User does not have access to this farm");
        }
        // check if user to add exists  
        UserBo userToAdd = await _userService.GetUserByEmail(addFarmerDTO.UserEmail)
            ?? throw new EntityNotFoundException("User not found");
        // chek if user is already part of the farm
        try
        {
            FarmUserBo? newUserFarm = await _farmUserService.GetUserFarm(farmId, userToAdd.Id);
            throw new BadHttpRequestException("User is already part of the farm");
        }
        catch (EntityNotFoundException)
        {
            await _farmUserService.AddFarmer(userFarm.Farm, userToAdd, addFarmerDTO.Role);
            return Ok();
        }
    }

    [HttpPut("{farmId}/removeFarmer")]
    public async Task RemoveFarmer([FromRoute] long farmId, [FromBody] long userId)
    {
        var user = await GetRequestDetails();

        // check if user is farm admin
        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);
        if (userFarm.FarmRole != FarmRoles.Admin)
        {
            throw new UnauthorizedAccessException("User does not have access to this farm");
        }

        // check if user to remove exists  
        // chek if user is already part of the farm
        var userFarmToRemove = await _farmUserService.GetUserFarm(farmId, userId);

        if (userFarm.Farm.Owner.Id == userFarmToRemove.User.Id)
        {
            throw new BadHttpRequestException("Cannot remove farm owner");
        }

        if (userFarmToRemove.FarmRole == FarmRoles.Admin && userFarm.Farm.Owner.Id != user.Id)
        {
            throw new BadHttpRequestException("Only farm owner can remove farm admin");
        }

        // throw new NotImplementedException();

        try
        {
            await _farmUserService.RemoveFarmer(userFarmToRemove);
        }
        catch (EntityNotFoundException)
        {
            throw new BadHttpRequestException("User is not part of the farm");
        }
    }

    [HttpPost("{farmId}/thresholdPresets")]
    public async Task<ActionResult<ThresholdPresetDto>> CreateThresholdPreset([FromRoute] long farmId, [FromBody] ThresholdPresetDto thresholdPresetDto)
    {

        var user = await GetRequestDetails();

        FarmUserBo farmUser = await _farmUserService.GetUserFarm(farmId, user.Id);
        if (!farmUser.FarmRole.Equals(FarmRoles.Admin))
        {
            throw new UnauthorizedAccessException("User does not have rights to change this farm");
        }

        ThresholdPresetBo thresholdPresetBo = ThresholdPresetBo.FromDto(thresholdPresetDto);

        ThresholdPresetDto dto = ThresholdPresetDto.FromBo(await _thresholdPresetService.CreateThresholdPreset(farmId, thresholdPresetBo));

        return Ok(dto);
    }

    [HttpGet("{farmId}/thresholdPresets")]
    public async Task<ActionResult<List<ThresholdPresetDto>>> GetThresholdPresets([FromRoute] long farmId, [FromQuery] String? name)
    {
        var user = await GetRequestDetails();

        // checks if user is part of the farm
        FarmUserBo farmUser = await _farmUserService.GetUserFarm(farmId, user.Id);
        List<ThresholdPresetDto> dtos;

        if (string.IsNullOrEmpty(name))
        {
            dtos
             = (await _thresholdPresetService.GetThresholdPresets(farmId)).Select(tp => ThresholdPresetDto.FromBo(tp)).ToList();
        }
        else
        {
            dtos
             = (await _thresholdPresetService.GetThresholdPresetsByName(farmId, name)).Select(tp => ThresholdPresetDto.FromBo(tp)).ToList();
        }


        return Ok(dtos);
    }

    [HttpDelete("{farmId}/thresholdPresets/{thresholdPresetId}")]

    public async Task<ActionResult> DeleteThresholdPreset([FromRoute] long farmId, [FromRoute] long thresholdPresetId)
    {
        var user = await GetRequestDetails();

        // checks if user is oart of the farm
        FarmUserBo farmUser = await _farmUserService.GetUserFarm(farmId, user.Id);

        if (!farmUser.FarmRole.Equals(FarmRoles.Admin))
        {
            throw new UnauthorizedAccessException("User does not have rights to change this farm");
        }

        await _thresholdPresetService.DeleteThresholdPreset(thresholdPresetId);

        return Ok();
    }

    [HttpPut("{farmId}/thresholdPresets/{thresholdPresetId}")]
    public async Task<ActionResult> UpdateThresholdPreset([FromRoute] long farmId, [FromRoute] long thresholdPresetId, [FromBody] ThresholdPresetDto thresholdPresetDto)
    {
        var user = await GetRequestDetails();

        // checks if user is oart of the farm
        FarmUserBo farmUser = await _farmUserService.GetUserFarm(farmId, user.Id);

        if (!farmUser.FarmRole.Equals(FarmRoles.Admin))
        {
            throw new UnauthorizedAccessException("User does not have rights to change this farm");
        }

        ThresholdPresetBo thresholdPresetBo = ThresholdPresetBo.FromDto(thresholdPresetDto);
        thresholdPresetBo.Id = thresholdPresetId;

        await _thresholdPresetService.UpdateThresholdPreset(thresholdPresetBo);

        return Ok();
    }
}