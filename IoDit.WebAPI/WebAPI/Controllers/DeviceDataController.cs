using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.DeviceData;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DeviceDataController : ControllerBase, IBaseController
{
    private readonly ILogger<DeviceDataController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IDeviceDataService _deviceDataService;
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;

    public DeviceDataController(
        ILogger<DeviceDataController> logger,
        IConfiguration configuration,
        IDeviceDataService deviceDataService,
        IIoDitRepository repository,
        IUserRepository userRepository,
        ICompanyUserRepository companyUserRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _deviceDataService = deviceDataService;
        _repository = repository;
        _userRepository = userRepository;
        _companyUserRepository = companyUserRepository;
    }

    [HttpPost("getDevicesData")]
    public async Task<IActionResult> GetDevicesData([FromBody] long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        return Ok(await _deviceDataService.GetDevicesData(companyUser.CompanyId));
    }

    [HttpPost("loadMoreDeviceData")]
    public async Task<IActionResult> LoadMoreDeviceData([FromBody] GetRangedDeviceDataRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        return Ok(await _deviceDataService.GetRangedDevicesData(request));
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