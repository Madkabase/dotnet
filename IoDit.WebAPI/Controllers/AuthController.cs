using System.Security.Claims;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.Utilities.Helpers;
using IoDit.WebAPI.Models.Auth;

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
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto model)
    {

        var result = await _authService.Login(model.Email, model.Password, model.DeviceIdentifier);

        return Ok(result);

    }

    /// <summary>
    /// Registers a new user
    /// </summary>
    /// <param name="model">The model containing the email, password, first name and last name</param>
    /// <returns>A registrationResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponseDto>> Register([FromBody] RegisterRequestDto model)
    {
        // Check if the password and the confirm password are the same
        if (model.Password != model.ConfirmPassword)
        {
            throw new BadHttpRequestException("Passwords do not match");
        }

        var result = await _authService.Register(model.Email, model.Password, model.FirstName, model.LastName);
        if (result.RegistrationFlowType != RegistrationFlowType.NewUser)
        {
            throw new BadHttpRequestException(result.Message);
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
    public async Task<ActionResult<ConfirmCodeResponseDto>> ConfirmAccount([FromBody] ConfirmCodeRequestDto model)
    {
        var result = await _authService.ConfirmCode(model.Email, model.Code);
        if (result.CodeConfirmationFlowType != ConfirmCodeFlowType.Success)
        {
            throw new BadHttpRequestException(result.Message);

        }
        return Ok(result);
    }

    /// <summary>
    /// let the user refresh its access token from the refresh token
    /// </summary>
    [AllowAnonymous]
    [HttpPost("refreshAccessToken")]
    public async Task<ActionResult<RefreshTokenResponseDto>> refreshAccessToken([FromBody] RefreshTokenRequestDto model)
    {
        var token = await _refreshTokenService.GetRefreshTokenByToken(model.RefreshToken);
        if (token == null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token");
        }
        if (token.DeviceIdentifier != model.DeviceIdentifier)
        {
            throw new UnauthorizedAccessException("Invalid device identifier");
        }
        if (_refreshTokenService.isExpired(token))
        {
            throw new UnauthorizedAccessException("Refresh token expired");
        }
        var newToken = await _refreshTokenService.GenerateRefreshToken(token.User, token.DeviceIdentifier);

        return Ok(new RefreshTokenResponseDto
        {
            RefreshToken = newToken.Token,
            AccessToken = _jwtHelper.GenerateJwtToken(token.User.Email)
        });
    }

    /// <summary>
    /// Send reset password link to the user
    /// </summary>
    /// <param name="model">The model containing the email</param>
    /// <returns>A resetPasswordResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("sendResetMail")]
    public async Task<ActionResult<SendResetPasswordMailResponseDto>> SendResetPassword([FromBody] SendResetPasswordMailRequestDto model)
    {
        var result = await _authService.SendResetPasswordLink(model.Email);

        return Ok(result);
    }

    /// <summary>
    /// Reset the password of the user
    /// </summary>
    /// <param name="model">The model containing the token, the new password and the confirmation password</param>
    /// <returns>A resetPasswordResponseDTO</returns>
    [AllowAnonymous]
    [HttpPost("resetPassword")]
    public async Task<ActionResult<ResetPasswordResponseDto>> ResetPassword([FromBody] ResetPasswordRequestDto model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            throw new BadHttpRequestException("Passwords do not match");
        }

        var result = await _authService.ResetPassword(model.Token, model.Password);
        return Ok(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<BO.UserBo> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Invalid user");
        }
        var user = await _userService.GetUserByEmail(userId);
        return user;
    }
}