using System.Security.Claims;
using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
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
    public async Task<ActionResult<GlobalThresholdPresetDto>> GetGlobalThreshold([FromQuery] string? name)
    {
        var user = await GetRequestDetails();

        List<GlobalThresholdPresetBo> globalThreshold = await _globalThresholdService.GetGlobalThresholdPresets(name);


        return Ok(globalThreshold.Select((g) => GlobalThresholdPresetDto.FromBo(g)).ToList());
    }

    [HttpPut("updateGlobalThreshold")]
    public async Task<ActionResult<GlobalThresholdPresetDto>> UpdateGlobalThreshold([FromBody] GlobalThresholdPresetDto globalThresholdDto)
    {
        var user = await GetRequestDetails();

        if (user.AppRole != AppRoles.AppAdmin)
        {
            throw new UnauthorizedAccessException("User does not have access to this farm");
        }

        var globalThreshold = await _globalThresholdService.UpdateGlobalThreshold(GlobalThresholdPresetBo.FromDto(globalThresholdDto));
        return Ok(GlobalThresholdPresetDto.FromBo(globalThreshold));
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