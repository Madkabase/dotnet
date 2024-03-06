using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.DTO.Device;

public class DeviceDataDTO
{
    public long Id { get; set; }
    public float Moisture1 { get; set; }
    public float Moisture2 { get; set; }
    public float Temperature1 { get; set; }
    public float Temperature2 { get; set; }
    public int Salinity1 { get; set; }
    public int Salinity2 { get; set; }

    public DateTime TimeStamp { get; set; }

    public static DeviceDataDTO empty()
    {
        return new DeviceDataDTO
        {
            Id = 0,
            Moisture1 = 0,
            Moisture2 = 0,
            Temperature1 = 0,
            Temperature2 = 0,
            Salinity1 = 0,
            Salinity2 = 0,
            TimeStamp = DateTime.Now
        };
    }

    public static DeviceDataDTO FromBo(DeviceDataBo deviceDataBo)
    {
        return new DeviceDataDTO
        {
            Id = deviceDataBo.Id,
            Moisture1 = deviceDataBo.Moisture1,
            Moisture2 = deviceDataBo.Moisture2,
            Temperature1 = deviceDataBo.Temperature1,
            Temperature2 = deviceDataBo.Temperature2,
            Salinity1 = deviceDataBo.Salinity1,
            Salinity2 = deviceDataBo.Salinity2,
            TimeStamp = deviceDataBo.TimeStamp
        };
    }
}
