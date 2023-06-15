namespace IoDit.WebAPI.WebAPI.Models.Company;

public class SubscriptionRequestResponseDto
{
    public long Id { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public long UserId { get; set; }
    public bool IsFulfilled { get; set; }
    public int MaxDevices { get; set; }
}