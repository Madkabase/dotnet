using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities.Company;

public class CompanyDeviceData : EntityBase, IEntity
{
    public long DeviceId { get; set; }
    public CompanyDevice Device { get; set; }
    public int Sensor1 { get; set; }
    public int Sensor2 { get; set; }
    public int BatteryLevel { get; set; }
    public float Temperature { get; set; }
    public DateTime TimeStamp { get; set; }
}