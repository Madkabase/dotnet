namespace IoDit.WebAPI.DTO.Device;

public class DeviceDataDTO
{
    public long Id { get; set; }
    public int Humidity1 { get; set; }
    public int Humidity2 { get; set; }
    public int BatteryLevel { get; set; }
    public float Temperature { get; set; }
    public DateTime TimeStamp { get; set; }

    internal static DeviceDataDTO FromEntity(Persistence.Entities.DeviceData deviceData)
    {
        return new DeviceDataDTO
        {
            Id = deviceData.Id,
            Humidity1 = deviceData.Humidity1,
            Humidity2 = deviceData.Humidity2,
            BatteryLevel = deviceData.BatteryLevel,
            Temperature = deviceData.Temperature,
            TimeStamp = deviceData.TimeStamp
        };
    }

    internal static DeviceDataDTO empty()
    {
        return new DeviceDataDTO
        {
            Id = 0,
            Humidity1 = 0,
            Humidity2 = 0,
            BatteryLevel = 0,
            Temperature = 0,
            TimeStamp = DateTime.Now
        };
    }
}
