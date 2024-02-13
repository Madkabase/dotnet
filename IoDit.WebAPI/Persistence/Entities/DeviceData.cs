using IoDit.WebAPI.Persistence.Entities.Base;

namespace IoDit.WebAPI.Persistence.Entities;

public class DeviceData : EntityBase, IEntity
{
    public string DevEUI { get; set; }
    public int Humidity1 { get; set; }
    public int Humidity2 { get; set; }
    public int? BatteryLevel { get; set; }
    public float Temperature1 { get; set; }
    public float Temperature2 { get; set; }
    public float Salinity1 { get; set; }
    public float Salinity2 { get; set; }
    public DateTime TimeStamp { get; set; }

    //to string
    public override string ToString()
    {
        return $"DeviceData: devEUI: {DevEUI}, h1: {Humidity1}, h2: {Humidity2}, batteryLevel: {BatteryLevel}, temp1: {Temperature1}, temp2: {Temperature2}, salinity1: {Salinity1}, salinity2: {Salinity2}, ts: {TimeStamp}";
    }

    // from Bo
    internal static DeviceData FromBo(BO.DeviceDataBo deviceData)
    {
        return new DeviceData
        {
            DevEUI = deviceData.DevEUI,
            Humidity1 = deviceData.Humidity1,
            Humidity2 = deviceData.Humidity2,
            Temperature1 = deviceData.Temperature1,
            Temperature2 = deviceData.Temperature2,
            Salinity1 = deviceData.Salinity1,
            Salinity2 = deviceData.Salinity2,
            TimeStamp = deviceData.TimeStamp
        };
    }
}