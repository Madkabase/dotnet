namespace IoDit.WebAPI.WebAPI.Models.DeviceData;

public class DeviceDataResponseDto
{
    public long Id { get; set; }
    public long DeviceId { get; set; }
    public int Sensor1 { get; set; }
    public int Sensor2 { get; set; }
    public int BatteryLevel { get; set; }
    public float Temperature { get; set; }
    public DateTime TimeStamp { get; set; }
}