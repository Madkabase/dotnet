using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class DeviceData : EntityBase, IEntity
{
    public string DevEUI { get; set; }
    public int Humidity1 { get; set; }
    public int Humidity2 { get; set; }
    public int BatteryLevel { get; set; }
    public float Temperature { get; set; }
    public DateTime TimeStamp { get; set; }

    //to string
    public override string ToString()
    {
        return $"DeviceData: devEUI: {DevEUI}, h1: {Humidity1}, h2: {Humidity2}, batteryLevel: {BatteryLevel}, temp: {Temperature}, ts: {TimeStamp}";
    }
}