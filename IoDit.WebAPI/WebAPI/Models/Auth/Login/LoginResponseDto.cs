namespace IoDit.WebAPI.WebAPI.Models.Auth.Login;
using IoDit.WebAPI.WebAPI.Models.User;

public class LoginResponseDto
{
  public string? Token { get; set; }
  public string? RefreshToken { get; set; }

  public UserResponseDto? User { get; set; }
}