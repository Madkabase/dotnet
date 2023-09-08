namespace IoDit.WebAPI.DTO.Auth;

public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string DeviceIdentifier { get; set; }
}