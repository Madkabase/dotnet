using System.Security.Claims;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[Route("[controller]")]
[Authorize]
[ApiController]
public class ThresholdPresetController : ControllerBase, IBaseController
{

    private readonly IUserService _userService;
    private readonly IThresholdPresetService _globalThresholdService;

    public ThresholdPresetController(
        IUserService userService,
        IThresholdPresetService globalThresholdService
    )
    {
        _userService = userService;
        _globalThresholdService = globalThresholdService;
    }


    [HttpGet("globalPresets")]
    public async Task<IActionResult> GetGlobalThreshold([FromQuery] String? name)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }

        var globalThreshold = await _globalThresholdService.GetGlobalThresholdPresets(name);


        return Ok(globalThreshold.Select(GlobalThresholdPresetDto.FromEntity));
    }

    [HttpPut("updateGlobalThreshold")]
    public async Task<IActionResult> UpdateGlobalThreshold([FromBody] GlobalThresholdPresetDto globalThresholdDto)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }
        if (user.AppRole != AppRoles.AppAdmin)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this farm" });
        }

        var globalThreshold = await _globalThresholdService.UpdateGlobalThreshold(globalThresholdDto);
        return Ok(GlobalThresholdPresetDto.FromEntity(globalThreshold));
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