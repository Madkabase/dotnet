using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Field;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FieldController : ControllerBase, IBaseController
{
    private readonly ILogger<FieldController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IFieldService _fieldService;
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;

    public FieldController(
        ILogger<FieldController> logger,
        IConfiguration configuration,
        IFieldService fieldService,
        IIoDitRepository repository,
        IUserRepository userRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _fieldService = fieldService;
        _repository = repository;
        _userRepository = userRepository;
    }

    [HttpPost("createField")]
    public async Task<IActionResult> CreateCompanyField([FromBody] CreateCompanyFieldRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        var companyFarmUser =
            await _repository.GetCompanyUserFarmUser(request.CompanyFarmId, request.CompanyUserId);
        if (companyFarmUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        if (companyUser.CompanyRole == CompanyRoles.CompanyOwner ||
            companyUser.CompanyRole == CompanyRoles.CompanyAdmin ||
            companyFarmUser.CompanyFarmRole == CompanyFarmRoles.FarmAdmin)
        {
            return Ok(await _fieldService.CreateCompanyField(new CreateCompanyField()
            {
                Email = user.Email,
                Name = request.Name,
                CompanyUserId = companyUser.Id,
                Geofence = request.Geofence,
                CompanyId = companyUser.CompanyId,
                CompanyFarmId = request.CompanyFarmId
            }));
        }

        return BadRequest("Cannot access this feature, please contact your company owner or company admin");
    }

    [HttpPost("updateGeofence")]
    public async Task<IActionResult> UpdateGeofence([FromBody] UpdateGeofenceRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        var companyFarmUser =
            await _repository.GetCompanyUserFarmUser(request.CompanyFarmId, request.CompanyUserId);
        if (companyFarmUser == null || companyFarmUser.CompanyFarmRole == CompanyFarmRoles.FarmUser)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }

        return Ok(await _fieldService.UpdateGeofence(new UpdateGeofence()
        {
            Email = user.Email,
            CompanyUserId = companyUser.Id,
            Geofence = request.Geofence,
            FieldId = request.FieldId,
            CompanyFarmId = request.CompanyFarmId
        }));
    }

    [HttpPost("getFields")]
    public async Task<IActionResult> GetFields([FromBody] long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _repository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null)
        {
            return BadRequest("Cannot access this feature, please contact your company owner or company admin");
        }


        return Ok(await _fieldService.GetFields(companyUser.CompanyId));
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