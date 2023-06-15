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
    private readonly IIoDitRepository _repository;

    public UserDeviceDataController(
        ILogger<UserDeviceDataController> logger,
        IConfiguration configuration, IUserDeviceDataService userDeviceDataService, IIoDitRepository repository)
    {
        _logger = logger;
        _configuration = configuration;
        _userDeviceDataService = userDeviceDataService;
        _repository = repository;
    }
    
    [HttpPost("getUserThresholds")]
    public async Task<IActionResult> GetUserThresholds([FromBody]long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, companyUserId);
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

        var user = await _repository.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }
}