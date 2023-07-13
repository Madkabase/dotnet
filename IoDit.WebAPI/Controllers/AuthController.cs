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
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IRefreshJwtService _refreshTokenService;
    private readonly IJwtHelper _jwtHelper;

    public AuthController(
        IUserService userService,
        IAuthService authService,
        IRefreshJwtService refreshTokenService,
        IJwtHelper jwtHelper
    )
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
            return BadRequest(new ErrorResponseDTO { Message = "Passwords do not match" });
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

    /// <summary>
    /// let the user refresh its access token from the refresh token
    /// </summary>
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

    /// <summary>
    /// Send reset password link to the user
    /// </summary>
    /// <param name="model">The model containing the email</param>
    /// <returns>A resetPasswordResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("sendResetMail")]
    public async Task<IActionResult> SendResetPassword([FromBody] SendResetPasswordMailRequestDto model)
    {
        var result = await _authService.SendResetPasswordLink(model.Email);
        if (result.FlowType != ResetPasswordFlowType.MailSent)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Message = result.Message
            });
        }
        return Ok(result);
    }

    /// <summary>
    /// Reset the password of the user
    /// </summary>
    /// <param name="model">The model containing the token, the new password and the confirmation password</param>
    /// <returns>A resetPasswordResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(new ErrorResponseDTO { Message = "Passwords do not match" });
        }

        var result = await _authService.ResetPassword(model.Token, model.Password);
        if (result.FlowType != ResetPasswordFlowType.PasswordReset)
        {
            return BadRequest(new ErrorResponseDTO
            {
                Message = result.Message
            });
        }
        return Ok(result);
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