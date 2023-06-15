using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.Auth;
using IoDit.WebAPI.WebAPI.Models.Auth.Login;
using IoDit.WebAPI.WebAPI.Models.Auth.Register;
using IoDit.WebAPI.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class AuthController : ControllerBase, IBaseController
{
    private readonly ILogger<AuthController> _logger;
    private readonly IIoDitRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly IJwtUtils _jwtUtils;

    public AuthController(
        ILogger<AuthController> logger,
        IIoDitRepository repository,
        IUserRepository userRepository,
        IAuthService authService,
        IJwtUtils jwtUtils)
    {
        _logger = logger;
        _repository = repository;
        _userRepository = userRepository;
        _authService = authService;
        _jwtUtils = jwtUtils;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var result = await _authService.Login(model);
        if (result == null)
        {
            return Unauthorized();
        }
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("renewPassword")]
    public async Task<IActionResult> RenewPassword()
    {
        _logger.LogTrace("password renew requested by deviceId");
        return Ok("renewPasswordEndpointCalled");
    }

    [AllowAnonymous]
    [HttpPost("confirmCode")]
    public async Task<IActionResult> ConfirmCode([FromBody] ConfirmCodeRequestDto request)
    {
        return Ok(await _authService.ConfirmCode(request));
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
    {
        return Ok(await _authService.Register(request));
    }

    [AllowAnonymous]
    [HttpPost("refreshAccessToken")]
    public async Task<IActionResult> RefreshAccessToken([FromBody] RefreshTokenRequestDto refreshTokenRequest)
    {
        if (string.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
        {
            return BadRequest("Refresh token is required.");
        }

        var refreshToken = await _repository.GetRefreshToken(refreshTokenRequest.RefreshToken);

        if (refreshToken == null)
        {
            return NotFound("Invalid refresh token.");
        }

        if (refreshToken.Expires < DateTime.UtcNow)
        {
            return BadRequest("Refresh token has expired.");
        }

        var user = await _userRepository.GetUserById(refreshToken.UserId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var newAccessToken = _jwtUtils.GenerateJwtToken(user.Email);

        return Ok(new { AccessToken = newAccessToken });
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