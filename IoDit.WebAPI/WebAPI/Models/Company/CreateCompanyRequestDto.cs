namespace IoDit.WebAPI.WebAPI.Models.Company;

public class CreateCompanyRequestDto
{
    public string Name { get; set; }
    public int MaxDevices { get; set; }
    public string Email { get; set; }
}