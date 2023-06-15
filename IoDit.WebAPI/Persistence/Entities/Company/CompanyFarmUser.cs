using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyFarmUser : EntityBase, IEntity
{
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public long CompanyUserId { get; set; }
    public CompanyUser CompanyUser { get; set; }
    public long CompanyFarmId { get; set; }
    public CompanyFarm CompanyFarm { get; set; }
    public CompanyFarmRoles CompanyFarmRole { get; set; }
}