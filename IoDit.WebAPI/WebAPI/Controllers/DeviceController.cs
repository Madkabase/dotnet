using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Device;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DeviceController : ControllerBase, IBaseController
{
    private readonly ILogger<DeviceController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IDeviceService _deviceService;
    private readonly IIoDitRepository _repository;

    public DeviceController(
        ILogger<DeviceController> logger,
        IConfiguration configuration, 
        IDeviceService deviceService, 
        IIoDitRepository repository)
    {
        _logger = logger;
        _configuration = configuration;
        _deviceService = deviceService;
        _repository = repository;
    }
    
    [HttpPost("createDevice")]
    public async Task<IActionResult> CreateDevice(CreateDeviceRequestDto request)
    {
        var user = await GetRequestDetails();
        
        if (user == null || user.AppRole == AppRoles.AppUser)
        {
            return BadRequest("Cannot find user entity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);

        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)//todo change roles?
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }
        
        return Ok(await _deviceService.CreateDevice(request));
    }
    
    [HttpPost("getDevices")]
    public async Task<IActionResult> GetDevices([FromBody]long companyUserId)
    {
        var user = await GetRequestDetails();
        
        if (user == null)
        {
            return BadRequest("Cannot find user entity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot find company user entity");
        }
        
        return Ok(await _deviceService.GetDevices(companyUser.CompanyId));
    }
    
    [HttpPost("assignToField")]
    public async Task<IActionResult> AssignToField([FromBody]AssignToFieldRequestDto request)
    {
        var user = await GetRequestDetails();
        
        if (user == null)
        {
            return BadRequest("Cannot find user entity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot find company user entity");
        }
        
        return Ok(await _deviceService.AssignToField(request));
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