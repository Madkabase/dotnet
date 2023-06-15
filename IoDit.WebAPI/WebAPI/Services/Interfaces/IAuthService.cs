using IoDit.WebAPI.WebAPI.Models.Auth.Login;
using IoDit.WebAPI.WebAPI.Models.Auth.Register;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IAuthService
{
    Task<RegistrationResponseDto> Register(RegistrationRequestDto request);
    Task<ConfirmCodeResponseDto> ConfirmCode(ConfirmCodeRequestDto request);
    Task<LoginResponseDto?> Login(LoginRequestDto request);
}