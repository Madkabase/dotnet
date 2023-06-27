using System.Security.Claims;
using IoDit.WebAPI.DTO;
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

    [HttpGet("details/{farmId}")]
    public async Task<IActionResult> GetFarmDetails([FromRoute] int farmId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }
        var userFarm = await _farmUserService.GetUserFarm(farmId, user.Id);
        if (userFarm == null || userFarm.Role != FarmRoles.Admin || userFarm.Role != FarmRoles.Admin || user.AppRole != AppRoles.AppAdmin)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this farm" });
        }

        var farm = await _farmService.getFarmDetailsById(farmId);
        return Ok(farm);
    }
}