using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TestController : ControllerBase, IBaseController
{
    private readonly ILogger<TestController> _logger;
    private readonly ITestService _testService;
    private readonly IIoDitRepository _repository;
    private readonly IConfiguration _configuration;
    
    public TestController(
        ILogger<TestController> logger,
        ITestService testService,
        IIoDitRepository repository,
        IConfiguration configuration)
    {
        _logger = logger;
        _testService = testService;
        _repository = repository;
        _configuration = configuration;
    }
    
    // [AllowAnonymous]//todo delete before pushing
    // [HttpGet("populate")]
    // public async Task<IActionResult> Populate()
    // {
    //     var user = await GetRequestDetails();
    //     if (user == null || user.AppRole == AppRoles.AppUser)
    //     {
    //         return Unauthorized("Cannot access this endpoint");
    //     }
    //     return Ok(await _testService.ClearAllDataAsyncAndPopulate());
    // }
    
    [AllowAnonymous]
    [HttpGet("testmail")]
    public async Task<IActionResult> TestMail()
    {
        return Ok(await _testService.TestMail());
    }
    
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        var user = await GetRequestDetails();
        if (user == null || user.AppRole == AppRoles.AppUser)
        {
            return BadRequest("Cannot access this endpoint");
        }
        return Ok(await _testService.Test());
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