using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.DTO.Device;

public class DeviceDataDTO
{
    public long Id { get; set; }
    public int Humidity1 { get; set; }
    public int Humidity2 { get; set; }
    public float Temperature1 { get; set; }
    public float Temperature2 { get; set; }
    public DateTime TimeStamp { get; set; }

    public static DeviceDataDTO empty()
    {
        return new DeviceDataDTO
        {
            Id = 0,
            Humidity1 = 0,
            Humidity2 = 0,
            Temperature1 = 0,
            TimeStamp = DateTime.Now
        };
    }

    public static DeviceDataDTO FromBo(DeviceDataBo deviceDataBo)
    {
        return new DeviceDataDTO
        {
            Id = deviceDataBo.Id,
            Humidity1 = deviceDataBo.Humidity1,
            Humidity2 = deviceDataBo.Humidity2,
            Temperature1 = deviceDataBo.Temperature1,
            Temperature2 = deviceDataBo.Temperature2,
            TimeStamp = deviceDataBo.TimeStamp
        };
    }
}
