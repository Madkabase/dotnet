using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class Company : EntityBase, IEntity
{
    public long OwnerId { get; set; }
    public User Owner { get; set; }
    public string CompanyName { get; set; }
    public string AppId { get; set; }//app id from Lorior
    public string AppName { get; set; }//app name from Loriot 
    public int MaxDevices { get; set; }//max devices in app
    public ICollection<CompanyUser> Users { get; set; } = new List<CompanyUser>();
    public ICollection<CompanyFarm> Farms { get; set; } = new List<CompanyFarm>();
    public ICollection<CompanyField> Fields { get; set; } = new List<CompanyField>();
    public ICollection<CompanyDevice> Devices { get; set; } = new List<CompanyDevice>();
    public ICollection<CompanyThresholdPreset> ThresholdPresets { get; set; } = new List<CompanyThresholdPreset>();
    public ICollection<CompanyFarmUser> FarmUsers { get; set; } = new List<CompanyFarmUser>();
}