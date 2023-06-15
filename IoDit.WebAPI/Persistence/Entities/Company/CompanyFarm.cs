using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyFarm : EntityBase, IEntity
{
    public string Name { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<CompanyField> Fields { get; set; } = new List<CompanyField>();
    public ICollection<CompanyDevice> Devices { get; set; } = new List<CompanyDevice>();
}