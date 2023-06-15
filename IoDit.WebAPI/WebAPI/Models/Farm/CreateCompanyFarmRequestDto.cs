namespace IoDit.WebAPI.WebAPI.Models.Farm;

public class CreateCompanyFarmRequestDto
{
    public string Name { get; set; }
    public long CompanyId { get; set; }
    public long CompanyUserId { get; set; }
}