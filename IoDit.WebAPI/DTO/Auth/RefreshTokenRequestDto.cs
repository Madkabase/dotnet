namespace IoDit.WebAPI.DTO.Auth;

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; }

    public string DeviceIdentifier { get; set; }
}