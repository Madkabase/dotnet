using IoDit.WebAPI.DTO.Auth;
using IoDit.WebAPI.Models.Auth;

namespace IoDit.WebAPI.Services;


// generate the interface for the AuthService class

public interface IAuthService
{
    public Task<LoginResponseDto?> Login(String email, String password, String DeviceId);

    public Task<RegisterResponseDto> Register(String email, String password, String firstName, String lastName);

    public Task<ConfirmCodeResponseDto> ConfirmCode(String email, long confirmationCode);
    public Task<SendResetPasswordMailResponseDto> SendResetPasswordLink(string email);
}