namespace IoDit.WebAPI.DTO.Auth;

public class ConfirmCodeRequestDto
{
    public string Email { get; set; }
    public long Code { get; set; }
}