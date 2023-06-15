using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.CompanyUser;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CompanyUserController : ControllerBase, IBaseController
{
    private readonly ILogger<CompanyUserController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ICompanyUserService _companyUserService;
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyUserRepository _companyUserRepository;

    public CompanyUserController(
        ILogger<CompanyUserController> logger,
        IConfiguration configuration,
         ICompanyUserService companyUserService,
        IIoDitRepository repository,
        IUserRepository userRepository,
        ICompanyUserRepository companyUserRepository
        )
    {
        _logger = logger;
        _configuration = configuration;
        _companyUserService = companyUserService;
        _repository = repository;
        _userRepository = userRepository;
        _companyUserRepository = companyUserRepository;
    }

    [HttpGet("getUserCompanyUsers")]
    public async Task<IActionResult> GetUserCompanyUsers()
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        return Ok(await _companyUserService.GetUserCompanyUsers(user.Email));
    }

    [HttpPost("getCompanyUsers")]
    public async Task<IActionResult> GetCompanyUsers([FromBody] long companyUserId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, companyUserId);
        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)
        {
            return BadRequest("Cannot access this feature, please contact your company admin");
        }

        return Ok(await _companyUserService.GetCompanyUsers(companyUser.CompanyId));
    }

    [HttpPost("inviteUser")]
    public async Task<IActionResult> InviteUser([FromBody] InviteUserRequestDto request)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest("Cannot find user identity");
        }

        var companyUser = await _companyUserRepository.GetCompanyUserForUserSecure(user.Email, request.CompanyUserId);
        if (companyUser == null || companyUser.CompanyRole == CompanyRoles.CompanyUser)
        {
            return BadRequest("Cannot access this feature, please contact your company owner");
        }

        if (request.CompanyRole <= companyUser.CompanyRole)
        {
            return BadRequest("Cannot invite users with same or higher role set");
        }

        var invitedUser = await _userRepository.GetUserByEmail(request.Email);
        if (invitedUser == null)
        {
            return NotFound("Cannot find user with this email on app");
        }

        var invitedCompanyUser = await _companyUserRepository.GetCompanyUserForUserByCompanyId(request.Email, request.CompanyId);
        if (invitedCompanyUser != null)
        {
            return Conflict("User already part of this company");
        }

        return Ok(await _companyUserService.InviteUser(request));
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