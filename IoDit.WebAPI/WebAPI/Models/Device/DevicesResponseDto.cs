using IoDit.WebAPI.WebAPI.Models.DeviceData;

namespace IoDit.WebAPI.WebAPI.Models.Device;

public class GetDevicesResponseDto
{
    public long Id { get; set; }
    public string DevEUI { get; set; }
    public string JoinEUI { get; set; }
    public string AppKey { get; set; }
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
    public long FarmId { get; set; }
    public long? FieldId { get; set; }
    public ICollection<DeviceDataResponseDto> DeviceData { get; set; } = new List<DeviceDataResponseDto>();
}