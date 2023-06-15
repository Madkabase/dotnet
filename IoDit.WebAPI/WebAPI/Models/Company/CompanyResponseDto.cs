namespace IoDit.WebAPI.WebAPI.Models.Company;

public class GetCompanyResponseDto
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public string CompanyName { get; set; }
    public string OwnerEmail { get; set; }
    public string AppName { get; set; }
    public string AppId { get; set; }
    public int MaxDevices { get; set; }
}