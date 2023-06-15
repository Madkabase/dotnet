using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyUser : EntityBase, IEntity
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public CompanyRoles CompanyRole { get; set; }
    public bool IsDefault { get; set; }
    public ICollection<CompanyFarmUser> CompanyFarmUsers { get; set; } = new List<CompanyFarmUser>();
    public ICollection<CompanyUserDeviceData> CompanyUserDeviceData { get; set; } = new List<CompanyUserDeviceData>();

}