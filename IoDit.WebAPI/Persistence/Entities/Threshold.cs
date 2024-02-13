using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Entities;

public class Threshold : EntityBase, IEntity
{
    public Field Field { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public float Temperature1Min { get; set; }
    public float Temperature1Max { get; set; }
    public float Temperature2Min { get; set; }
    public float Temperature2Max { get; set; }
    public float Salinity1Min { get; set; }
    public float Salinity1Max { get; set; }
    public float Salinity2Min { get; set; }
    public float Salinity2Max { get; set; }
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
            Temperature1Min = thresholdDto.Temperature1Min,
            Temperature1Max = thresholdDto.Temperature1Max,
            Temperature2Min = thresholdDto.Temperature2Min,
            Temperature2Max = thresholdDto.Temperature2Max,
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
            Temperature1Min = threshold.Temperature1Min,
            Temperature1Max = threshold.Temperature1Max,
            Temperature2Min = threshold.Temperature2Min,
            Temperature2Max = threshold.Temperature2Max,
            MainSensor = threshold.MainSensor
        };
    }
}