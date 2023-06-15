using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Company;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CompanyController : ControllerBase, IBaseController
{
    private readonly ILogger<CompanyController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ICompanyService _companyService;
    private readonly IUtilsRepository _utilsRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;

    public CompanyController(
        ILogger<CompanyController> logger,
        IConfiguration configuration,
        ICompanyService companyService,
        IUtilsRepository repository,
        IUserRepository userRepository,
        ICompanyUserRepository companyUserRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _companyService = companyService;
        _utilsRepository = repository;
        _userRepository = userRepository;
        _companyUserRepository = companyUserRepository;
    }

    [HttpPost("createCompany")]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null || user.AppRole == AppRoles.AppUser)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }

        return Ok(await _companyService.CreateCompany(request));
    }

    [HttpPost("requestSubscription")]
    public async Task<IActionResult> RequestSubscription([FromBody] CreateRequestSubscriptionRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }

        return Ok(await _companyService.CreateSubscriptionRequest(request));
    }

    [HttpPost("getCompany")]
    public async Task<IActionResult> GetCompany([FromBody] long companyUserId)
    {
        var user = await GetRequestDetails();

        if (user == null)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }
        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null || companyUser.CompanyRole != CompanyRoles.CompanyOwner)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }

        var response = await _companyService.GetCompany(companyUser.CompanyId);
        if (response == null)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }
        return Ok(response);
    }

    [HttpGet("getCompanies")]
    public async Task<IActionResult> GetCompanies()
    {
        var user = await GetRequestDetails();

        if (user == null || user.AppRole != AppRoles.AppAdmin)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }

        return Ok(await _companyService.GetCompanies());
    }


    [HttpGet("getSubRequests")]
    public async Task<IActionResult> GetSubRequests()
    {
        var user = await GetRequestDetails();

        if (user == null || user.AppRole != AppRoles.AppAdmin)
        {
            return BadRequest("Cannot access this feature, please, contact app administrator");
        }

        return Ok(await _companyService.GetSubscriptionRequests());
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