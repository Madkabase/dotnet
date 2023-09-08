using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class SubscriptionRequest : EntityBase, IEntity
{
    public string FarmName { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public bool IsFulfilled { get; set; }
    public int MaxDevices { get; set; }
}