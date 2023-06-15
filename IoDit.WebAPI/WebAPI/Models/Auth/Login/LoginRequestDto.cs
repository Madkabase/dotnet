namespace IoDit.WebAPI.WebAPI.Models.Auth.Login;

public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string DeviceIdentifier { get; set; }
}