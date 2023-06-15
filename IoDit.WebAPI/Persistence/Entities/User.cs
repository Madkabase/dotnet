using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities;

public class User : EntityBase, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; }
    public long ConfirmationCode { get; set; }
    public AppRoles AppRole { get; set; }
    public DateTime ConfirmationExpirationDate { get; set; }
    public int ConfirmationTriesCounter { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
    public ICollection<Company.Company> Companies { get; set; } = new List<Company.Company>();
    public ICollection<SubscriptionRequest> SubscriptionRequests { get; set; } = new List<SubscriptionRequest>();
}
