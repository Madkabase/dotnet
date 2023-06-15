namespace IoDit.WebAPI.WebAPI.Models.DeviceData;

public class GetRangedDeviceDataRequestDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string DevEui { get; set; }
    public long CompanyUserId { get; set; }
}