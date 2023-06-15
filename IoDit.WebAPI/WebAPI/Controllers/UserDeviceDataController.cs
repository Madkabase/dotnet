using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserDeviceDataController : ControllerBase, IBaseController
{
    private readonly ILogger<UserDeviceDataController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserDeviceDataService _userDeviceDataService;
    private readonly IUtilsRepository _utilsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;

    public UserDeviceDataController(
        ILogger<UserDeviceDataController> logger,
        IConfiguration configuration,
        IUserDeviceDataService userDeviceDataService,
        IUtilsRepository repository,
        IUserRepository userRepository,
        ICompanyUserRepository companyUserRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _userDeviceDataService = userDeviceDataService;
        _utilsRepository = repository;
        _userRepository = userRepository;
        _companyUserRepository = companyUserRepository;
    }

    [HttpPost("getUserThresholds")]
    public async Task<IActionResult> GetUserThresholds([FromBody] long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company admin");
        }

        return Ok(await _userDeviceDataService.GetCompanyUserThresholds(companyUserId));
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