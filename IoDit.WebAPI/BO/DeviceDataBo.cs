using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.BO;

public class DeviceDataBo
{
    public long Id;
    public string DevEUI { get; set; }
    public int Humidity1 { get; set; }
    public int Humidity2 { get; set; }
    public float Temperature1 { get; set; }
    public float Temperature2 { get; set; }
    public float Salinity1 { get; set; }
    public float Salinity2 { get; set; }
    public DateTime TimeStamp { get; set; }

    public DeviceDataBo(long id, string devEUI, int humidity1, int humidity2, float temperature1, float temperature2, float salinity1, float salinity2, DateTime timeStamp)
    {
        Id = id;
        DevEUI = devEUI;
        Humidity1 = humidity1;
        Humidity2 = humidity2;
        Temperature1 = temperature1;
        Temperature2 = temperature2;
        Salinity1 = salinity1;
        Salinity2 = salinity2;
        TimeStamp = timeStamp;
    }

    //from entity
    public static DeviceDataBo FromEntity(DeviceData entity)
    {
        return new DeviceDataBo(entity.Id, entity.DevEUI, entity.Humidity1, entity.Humidity2, entity.Temperature1, entity.Temperature2, entity.Salinity1, entity.Salinity2, entity.TimeStamp);
    }

    //from Dto
    public static DeviceDataBo FromDto(DeviceDataDTO dto)
    {
        return new DeviceDataBo(dto.Id, "", dto.Humidity1, dto.Humidity2, dto.Temperature1, dto.Temperature2, dto.Salinity1, dto.Salinity2, dto.TimeStamp);
    }


}