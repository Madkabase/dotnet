using System.Security.Claims;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
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
    public async Task<ActionResult<List<Farm>>> GetMyFarms()
    {
        var user = await GetRequestDetails();

        var farms = await _farmService.getUserFarms(UserDto.FromEntity(user));
        return Ok(farms);
    }

    [HttpGet("details/{farmId}")]
    public async Task<ActionResult<FarmDTO>> GetFarmDetails([FromRoute] int farmId)
    {
        var user = await GetRequestDetails();

        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);

        // check if user not null or farm admin or app admin

        if (userFarm == null && userFarm?.FarmRole != FarmRoles.Admin && user.AppRole != AppRoles.AppAdmin)
        {
            throw new UnauthorizedAccessException("User does not have access to this farm");
        }

        var farm = await _farmService.getFarmDetailsById(farmId);
        if (farm == null)
        {
            throw new EntityNotFoundException("Farm not found");
        }

        farm.isRequesterAdmin = userFarm?.FarmRole == FarmRoles.Admin || user.AppRole == AppRoles.AppAdmin;

        farm.Users?.ForEach(u =>
        {
            // remove farm from user dto
            u.Farm = null;
            // if requester is not admin, remove user details
            if (!farm.isRequesterAdmin)
            {
                u.User = new UserDto { };
            }
        }
        );
        farm.Fields?.ForEach(f =>
        {
            f.OverallMoistureLevel = _fieldService.CalculateOverAllMoistureLevel(f.Devices);
        });
        return Ok(farm);
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
        var userToAdd = await _userService.GetUserByEmail(addFarmerDTO.UserEmail);
        if (userToAdd == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        // chek if user is already part of the farm
        var newUserFarm = await _farmUserService.GetUserFarm(farmId, userToAdd.Id);
        if (newUserFarm != null)
        {
            throw new BadHttpRequestException("User is already part of the farm");
        }

        FarmUser userFarmToAdd = await _farmUserService.AddFarmer(userFarm.Farm, userToAdd, addFarmerDTO.Role);
        if (userFarmToAdd == null)
        {
            throw new Exception("Error adding user to farm");
        }

        return Ok();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<User> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        var user = await _userService.GetUserByEmail(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        return user;
    }


}