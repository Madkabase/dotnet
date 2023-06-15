namespace IoDit.WebAPI.WebAPI.Models.Auth.Register;

public class ConfirmCodeRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long Code { get; set; }
}