using System.Security.Claims;
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

    public FarmController(
        IFarmService farmService,
        IUserService userService,
        IFarmUserService farmUserService
    )
    {
        _farmService = farmService;
        _userService = userService;
        _farmUserService = farmUserService;
    }

    [HttpGet("myFarms")]
    public async Task<IActionResult> GetMyFarms()
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }
        var farms = await _farmService.getUserFarms(UserDto.FromEntity(user));
        return Ok(farms);
    }

    [HttpGet("details/{farmId}")]
    public async Task<IActionResult> GetFarmDetails([FromRoute] int farmId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }
        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);

        // check if user not null or farm admin or app admin

        if (userFarm == null && userFarm?.FarmRole != FarmRoles.Admin && user.AppRole != AppRoles.AppAdmin)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this farm" });
        }

        var farm = await _farmService.getFarmDetailsById(farmId);
        if (farm == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "Farm not found" });
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
        return Ok(farm);
    }

    [HttpPut("{farmId}/addFarmer")]
    public async Task<IActionResult> AddFarmer([FromRoute] int farmId, [FromBody] AddFarmerDTO addFarmerDTO)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return Unauthorized(new ErrorResponseDTO { Message = "User not found" });
        }
        // check if user is farm admin
        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);
        if (userFarm == null || userFarm.FarmRole != FarmRoles.Admin)
        {
            return Unauthorized(new ErrorResponseDTO { Message = "User does not have access to this farm" });
        }
        // check if user to add exists  
        var userToAdd = await _userService.GetUserByEmail(addFarmerDTO.UserEmail);
        if (userToAdd == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }
        // chek if user is already part of the farm
        var newUserFarm = await _farmUserService.GetUserFarm(farmId, userToAdd.Id);
        if (newUserFarm != null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User is already part of the farm" });
        }

        FarmUser userFarmToAdd = await _farmUserService.AddFarmer(userFarm.Farm, userToAdd, addFarmerDTO.Role);
        if (userFarmToAdd == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "Error adding user to farm" });
        }

        return Ok();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<User?> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            return null;
        }
        var user = await _userService.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }



}