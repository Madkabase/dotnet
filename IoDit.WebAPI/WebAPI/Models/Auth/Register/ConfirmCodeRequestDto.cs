namespace IoDit.WebAPI.WebAPI.Models.Auth.Register;

public class ConfirmCodeRequestDto
{
    public string Email { get; set; }
    public long Code { get; set; }
}