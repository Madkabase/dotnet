using System.Security.Claims;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FarmController : ControllerBase, IBaseController
{
    FarmService _farmService;
    UserService _userService;

    public FarmController(FarmService farmService,
        UserService userService
        )
    {
        _farmService = farmService;
        _userService = userService;
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
}