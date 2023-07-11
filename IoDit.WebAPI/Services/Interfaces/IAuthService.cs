using IoDit.WebAPI.DTO.Auth;
using IoDit.WebAPI.Models.Auth;

namespace IoDit.WebAPI.Services;


// generate the interface for the AuthService class

public interface IAuthService
{
    /// <summary>
    /// Logs in a user
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="DeviceId">The device id of the user</param>
    public Task<LoginResponseDto?> Login(String email, String password, String DeviceId);

    /// <summary>
    /// Registers a user
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="firstName">The first name of the user</param>
    /// <param name="lastName">The last name of the user</param>
    /// <returns></returns>

    public Task<RegisterResponseDto> Register(String email, String password, String firstName, String lastName);
    /// <summary>
    /// Confirms the code sent to the user's email  
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="confirmationCode">The confirmation code sent to the user's email</param>
    /// <returns></returns>
    public Task<ConfirmCodeResponseDto> ConfirmCode(String email, long confirmationCode);
    /// <summary>
    /// Sends a reset password link to the user's email
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <returns></returns>
    public Task<SendResetPasswordMailResponseDto> SendResetPasswordLink(string email);
    /// <summary>
    /// Resets the user's password
    /// </summary>
    /// <param name="token">The token sent to the user's email</param>
    /// <param name="newPassword">The new password of the user</param>
    /// <returns></returns>
    Task<ResetPasswordResponseDto> ResetPassword(string token, String newPassword);
}