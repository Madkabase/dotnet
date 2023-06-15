using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyUserDeviceData : EntityBase, IEntity
{
    public long DeviceId { get; set; }
    public CompanyDevice Device { get; set; }
    public long UserId { get; set; }
    public CompanyUser User { get; set; }
    public long Humidity1Min { get; set; }
    public long Humidity1Max { get; set; }
    public long Humidity2Min { get; set; }
    public long Humidity2Max { get; set; }
    public long BatteryLevelMin { get; set; }
    public long BatteryLevelMax { get; set; }
    public long TemperatureMin { get; set; }
    public long TemperatureMax { get; set; }
}