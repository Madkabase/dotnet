using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.BO;

public class DeviceDataBo
{
    public long Id;
    public string DevEUI { get; set; }
    public int Humidity1 { get; set; }
    public int Humidity2 { get; set; }
    public int? BatteryLevel { get; set; }
    public float Temperature { get; set; }
    public DateTime TimeStamp { get; set; }

    public DeviceDataBo(long id, string devEUI, int humidity1, int humidity2,  float temperature, DateTime timeStamp)
    {
        Id = id;
        DevEUI = devEUI;
        Humidity1 = humidity1;
        Humidity2 = humidity2;
        Temperature = temperature;
        TimeStamp = timeStamp;
    }

    //from entity
    public static DeviceDataBo FromEntity(DeviceData entity)
    {
        return new DeviceDataBo(entity.Id, entity.DevEUI, entity.Humidity1, entity.Humidity2, entity.Temperature, entity.TimeStamp);
    }

    //from Dto
    public static DeviceDataBo FromDto(DeviceDataDTO dto)
    {
        return new DeviceDataBo(dto.Id, "", dto.Humidity1, dto.Humidity2, dto.Temperature, dto.TimeStamp);
    }


}