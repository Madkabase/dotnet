using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.BO;

public class DeviceDataBo
{
    public long Id;
    public string DevEUI { get; set; }
    public float Moisture1 { get; set; }
    public float Moisture2 { get; set; }
    public float Temperature1 { get; set; }
    public float Temperature2 { get; set; }
    public int Salinity1 { get; set; }
    public int Salinity2 { get; set; }
    public DateTime TimeStamp { get; set; }

    public DeviceDataBo(long id, string devEUI, float humidity1, float humidity2, float temperature1, float temperature2, int salinity1, int salinity2, DateTime timeStamp)
    {
        Id = id;
        DevEUI = devEUI;
        Moisture1 = humidity1;
        Moisture2 = humidity2;
        Temperature1 = temperature1;
        Temperature2 = temperature2;
        Salinity1 = salinity1;
        Salinity2 = salinity2;
        TimeStamp = timeStamp;
    }

    //from entity
    public static DeviceDataBo FromEntity(DeviceData entity)
    {
        return new DeviceDataBo(entity.Id, entity.DevEUI, entity.Moisture1, entity.Moisture2, entity.Temperature1, entity.Temperature2, entity.Salinity1, entity.Salinity2, entity.TimeStamp);
    }

    //from Dto
    public static DeviceDataBo FromDto(DeviceDataDTO dto)
    {
        return new DeviceDataBo(dto.Id, "", dto.Moisture1, dto.Moisture2, dto.Temperature1, dto.Temperature2, dto.Salinity1, dto.Salinity2, dto.TimeStamp);
    }


}