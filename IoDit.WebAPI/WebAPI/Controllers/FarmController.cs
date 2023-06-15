using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Farm;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FarmController : ControllerBase, IBaseController
{
    private readonly ILogger<FarmController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IFarmService _farmService;
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;

    public FarmController(
        ILogger<FarmController> logger,
        IConfiguration configuration,
        IFarmService farmService,
        IIoDitRepository repository,
        IUserRepository userRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _farmService = farmService;
        _repository = repository;
        _userRepository = userRepository;
    }

    [HttpPost("createFarm")]
    public async Task<IActionResult> CreateFarm(CreateCompanyFarmRequestDto request)
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

        return Ok(await _farmService.CreateCompanyFarm(new CreateCompanyFarm()
        { Email = user.Email, Name = request.Name, CompanyId = companyUser.CompanyId }));
    }


    [HttpPost("getFarms")]
    public async Task<IActionResult> GetCompanyFarms([FromBody] long companyUserId)
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

        return Ok(await _farmService.GetCompanyFarms(companyUserId));
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