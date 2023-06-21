using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.Utilities.Helpers;
using IoDit.WebAPI.DTO;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase, IBaseController
{
    private readonly UserService _userService;
    private readonly AuthService _authService;
    private readonly RefreshJwtService _refreshTokenService;
    private readonly JwtHelper _jwtHelper;

    public AuthController(UserService userService,
        AuthService authService,
        RefreshJwtService refreshTokenService,
        JwtHelper jwtHelper)
    {
        _userService = userService;
        _authService = authService;
        _refreshTokenService = refreshTokenService;
        _jwtHelper = jwtHelper;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        try
        {
            var result = await _authService.Login(model.Email, model.Password, model.DeviceIdentifier);

            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(new ErrorResponseDTO { Message = e.Message });
        }
        catch (Exception e)
        {
            return BadRequest(new ErrorResponseDTO { Message = e.Message });
        }
    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="model">The model containing the email, password, first name and last name</param>
    /// <returns>A registrationResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
    {

        // Check if the password and the confirm password are the same
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest("Passwords do not match");
        }

        var result = await _authService.Register(model.Email, model.Password, model.FirstName, model.LastName);
        if (result.RegistrationFlowType != RegistrationFlowType.NewUser)
        {
            return BadRequest(new ErrorResponseDTO { Message = result.Message });
        }
        return Ok(result);
    }

    /// <summary>
    /// Confirms the account of a user
    /// </summary>
    /// <param name="model">The model containing the email and the code</param>
    /// <returns>A confirmationResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("confirmAccount")]
    public async Task<IActionResult> ConfirmAccount([FromBody] ConfirmCodeRequestDto model)
    {
        var result = await _authService.ConfirmCode(model.Email, model.Code);
        if (result.CodeConfirmationFlowType != ConfirmCodeFlowType.Success)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Message = result.Message
            });
        }
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("refreshAccessToken")]
    public async Task<IActionResult> refreshAccessToken([FromBody] RefreshTokenRequestDto model)
    {
        var token = await _refreshTokenService.GetRefreshTokenByToken(model.RefreshToken);
        if (token == null)
        {
            return Unauthorized(new ErrorResponseDTO { Message = "Invalid refresh token" });
        }
        if (token.DeviceIdentifier != model.DeviceIdentifier)
        {
            return Unauthorized(new ErrorResponseDTO { Message = "Invalid device identifier" });
        }
        if (await _refreshTokenService.isExpired(token))
        {
            return Unauthorized(new ErrorResponseDTO { Message = "Refresh token expired" });
        }
        var newToken = await _refreshTokenService.GenerateRefreshToken(token.User, token.DeviceIdentifier);

        return Ok(new
        {
            refreshToken = newToken.Token,
            accessToken = _jwtHelper.GenerateJwtToken(token.User.Email)
        });
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
        var user = await _userService.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }
}