using IoDit.WebAPI.WebAPI.DTO.User;

namespace IoDit.WebAPI.WebAPI.Models.Auth.Login;

public class LoginResponseDto
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }

    public UserDto? User { get; set; }
}