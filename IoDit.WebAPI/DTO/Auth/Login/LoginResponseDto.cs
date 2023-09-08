
using IoDit.WebAPI.DTO.User;

namespace IoDit.WebAPI.DTO.Auth;

public class LoginResponseDto
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}