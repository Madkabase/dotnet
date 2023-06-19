using System.Security.Claims;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Types;
using IoDit.WebAPI.WebAPI.Models.Auth.Login;
using IoDit.WebAPI.WebAPI.Models.Auth.Register;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase, IBaseController
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public AuthController(UserService userService,
        AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

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
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return BadRequest("Something went wrong");
    }

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
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("confirmAccount")]
    public async Task<IActionResult> ConfirmAccount([FromBody] ConfirmCodeRequestDto model)
    {
        // try
        // {
        var result = await _authService.ConfirmCode(model.Email, model.Code);
        if (result.CodeConfirmationFlowType != ConfirmCodeFlowType.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
        // }
        // catch (UnauthorizedAccessException e)
        // {
        //     return BadRequest(e.Message);
        // }
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