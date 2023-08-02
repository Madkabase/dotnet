using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Entities;

public class Threshold : EntityBase, IEntity
{
    public Field? Field { get; set; } = null;
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }
    public int BatteryLevelMin { get; set; }
    public int BatteryLevelMax { get; set; }
    public MainSensor MainSensor { get; set; }

    // from dto
    public static Threshold FromDto(ThresholdDto thresholdDto)
    {
        return new Threshold
        {
            Id = thresholdDto.Id,
            Humidity1Min = thresholdDto.Humidity1Min,
            Humidity1Max = thresholdDto.Humidity1Max,
            Humidity2Min = thresholdDto.Humidity2Min,
            Humidity2Max = thresholdDto.Humidity2Max,
            TemperatureMin = thresholdDto.TemperatureMin,
            TemperatureMax = thresholdDto.TemperatureMax,
            BatteryLevelMin = thresholdDto.BatteryLevelMin,
            BatteryLevelMax = thresholdDto.BatteryLevelMax,
            MainSensor = thresholdDto.MainSensor
        };
    }

    public static Threshold FromBo(ThresholdBo threshold)
    {
        return new Threshold
        {
            Id = threshold.Id,
            Humidity1Min = threshold.Humidity1Min,
            Humidity1Max = threshold.Humidity1Max,
            Humidity2Min = threshold.Humidity2Min,
            Humidity2Max = threshold.Humidity2Max,
            TemperatureMin = threshold.TemperatureMin,
            TemperatureMax = threshold.TemperatureMax,
            BatteryLevelMin = threshold.BatteryLevelMin,
            BatteryLevelMax = threshold.BatteryLevelMax,
            MainSensor = threshold.MainSensor
        };
    }
}