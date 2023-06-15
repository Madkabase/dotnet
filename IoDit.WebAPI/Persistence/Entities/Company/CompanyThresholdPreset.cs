using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyThresholdPreset : EntityBase, IEntity
{
    public string Name { get; set; }
    public long DefaultHumidity1Min { get; set; }
    public long DefaultHumidity1Max { get; set; }
    public long DefaultHumidity2Min { get; set; }
    public long DefaultHumidity2Max { get; set; }
    public long DefaultBatteryLevelMin { get; set; }
    public long DefaultBatteryLevelMax { get; set; }
    public long DefaultTemperatureMin { get; set; }
    public long DefaultTemperatureMax { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
}