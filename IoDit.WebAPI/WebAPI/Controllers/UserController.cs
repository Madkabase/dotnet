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
public class UserController : ControllerBase, IBaseController
{
    private readonly ILogger<UserController> _logger;
    private readonly IIoDitRepository _repository;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    
    public UserController(
        ILogger<UserController> logger,
        IIoDitRepository repository,
        IUserService userService,
        IConfiguration configuration)
    {
        _logger = logger;
        _repository = repository;
        _userService = userService;
        _configuration = configuration;
    }
    
        
    [HttpGet("getUser")]
    public async Task<IActionResult> GetDevices()
    {
        var user = await GetRequestDetails();
        
        if (user == null)
        {
            return BadRequest("Cannot find user entity");
        }
        
        return Ok(await _userService.GetUser(user.Email));
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