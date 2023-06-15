namespace IoDit.WebAPI.WebAPI.Models.UserDeviceData;

public class UserDeviceDataResponseDto
{
    public long Id { get; set; }
    public long DeviceId { get; set; }
    public long UserId { get; set; }
    public long Humidity1Min { get; set; }
    public long Humidity1Max { get; set; }
    public long Humidity2Min { get; set; }
    public long Humidity2Max { get; set; }
    public long BatteryLevelMin { get; set; }
    public long BatteryLevelMax { get; set; }
    public long TemperatureMin { get; set; }
    public long TemperatureMax { get; set; }
}