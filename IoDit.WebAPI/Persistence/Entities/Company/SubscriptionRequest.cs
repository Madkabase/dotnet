using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class SubscriptionRequest: EntityBase, IEntity
{
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public bool IsFulfilled { get; set; }
    public int MaxDevices { get; set; }
}