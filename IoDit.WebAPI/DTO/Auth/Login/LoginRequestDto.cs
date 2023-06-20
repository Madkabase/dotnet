namespace IoDit.WebAPI.DTO.Auth;

public class LoginRequestDto
{
    public String Email { get; set; }
    public String Password { get; set; }
    public String DeviceIdentifier { get; set; }
}