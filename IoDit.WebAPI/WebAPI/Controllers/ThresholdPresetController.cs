using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.ThresholdPreset;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ThresholdPresetController : ControllerBase, IBaseController
{
    private readonly ILogger<ThresholdPresetController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IThresholdPresetService _thresholdPresetService;
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;

    public ThresholdPresetController(
        ILogger<ThresholdPresetController> logger,
        IConfiguration configuration,
        IThresholdPresetService thresholdPresetService,
        IIoDitRepository repository,
        IUserRepository userRepository,
        ICompanyUserRepository companyUserRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _thresholdPresetService = thresholdPresetService;
        _repository = repository;
        _userRepository = userRepository;
        _companyUserRepository = companyUserRepository;
    }

    //CreateThresholdPresetRequestDto

    [HttpPost("createThresholdPreset")]
    public async Task<IActionResult> CreateThresholdPreset([FromBody] CreateThresholdPresetRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        return Ok(await _thresholdPresetService.CreateThresholdPreset(new CreateThresholdPreset()
        {
            Name = request.Name,
            CompanyId = companyUser.CompanyId,
            DefaultHumidity1Max = request.DefaultHumidity1Max,
            DefaultHumidity1Min = request.DefaultHumidity1Min,
            DefaultHumidity2Max = request.DefaultHumidity2Max,
            DefaultHumidity2Min = request.DefaultHumidity2Min,
            DefaultTemperatureMax = request.DefaultTemperatureMax,
            DefaultTemperatureMin = request.DefaultTemperatureMin,
            DefaultBatteryLevelMax = request.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = request.DefaultBatteryLevelMin
        }));
    }

    [HttpPost("getThresholdPresets")]
    public async Task<IActionResult> GetThresholdPresets([FromBody] long companyId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserByCompanyId(user.Email, companyId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        return Ok(await _thresholdPresetService.GetThresholdPresets(companyId));
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

        var user = await _userRepository.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }
}