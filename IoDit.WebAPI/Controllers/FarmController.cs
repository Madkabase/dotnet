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
    private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly IFarmService _farmService;
    private readonly IUserService _userService;
    private readonly IFarmUserService _farmUserService;
    private readonly IFieldService _fieldService;

    public FarmController(
        IFarmService farmService,
        IUserService userService,
        IFarmUserService farmUserService,
        IFieldService fieldService
    )
    {
        _farmService = farmService;
        _userService = userService;
        _farmUserService = farmUserService;
        _fieldService = fieldService;
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
        FarmDto farmDto = new FarmDto();

        farmDto.isRequesterAdmin = userFarm?.FarmRole == FarmRoles.Admin || user.AppRole == AppRoles.AppAdmin;
        farmDto.Id = farm.Id;
        farmDto.Name = farm.Name;
        farmDto.AppId = farm.AppId;
        farmDto.AppName = farm.AppName;
        farmDto.MaxDevices = farm.MaxDevices;
        farmDto.Owner = UserDto.FromBo(farm.Owner);

        //! FIX THIS by adding a new method to get farm users
        farmDto.Users = new List<FarmUserDto>();

        farmDto.Fields = farm.Fields?.Select(f =>
            new FieldDto
            {
                Id = f.Id,
                Name = f.Name,
                Geofence = f.Geofence,
                Threshold = ThresholdDto.FromBo(f.Threshold),
                OverallMoistureLevel = _fieldService.CalculateOverAllMoistureLevel(f.Devices.ToList(), f.Threshold)
            }
        ).ToList();

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
        UserBo userToAdd = await _userService.GetUserByEmail(addFarmerDTO.UserEmail);
        if (userToAdd == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        // chek if user is already part of the farm
        FarmUserBo? newUserFarm = await _farmUserService.GetUserFarm(farmId, userToAdd.Id);
        if (newUserFarm != null)
        {
            throw new BadHttpRequestException("User is already part of the farm");
        }

        FarmUserBo userFarmToAdd = await _farmUserService.AddFarmer(userFarm.Farm, userToAdd, addFarmerDTO.Role);
        return Ok();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<BO.UserBo> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        var user = await _userService.GetUserByEmail(userId);
        return user;
    }


}