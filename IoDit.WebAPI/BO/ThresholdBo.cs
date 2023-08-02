using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class ThresholdBo
{
    public long Id { get; set; }
    public FieldBo Field { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public int BatteryLevelMin { get; set; }
    public int BatteryLevelMax { get; set; }
    public double TemperatureMin { get; set; }
    public double TemperatureMax { get; set; }
    public MainSensor MainSensor { get; set; }

    public ThresholdBo(long id, FieldBo field, int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, int batteryLevelMin, int batteryLevelMax, double temperatureMin, double temperatureMax, MainSensor mainSensor)
    {
        Id = id;
        Field = field;
        Humidity1Min = humidity1Min;
        Humidity1Max = humidity1Max;
        Humidity2Min = humidity2Min;
        Humidity2Max = humidity2Max;
        BatteryLevelMin = batteryLevelMin;
        BatteryLevelMax = batteryLevelMax;
        TemperatureMin = temperatureMin;
        TemperatureMax = temperatureMax;
        MainSensor = mainSensor;
    }

    // from dto
    public static ThresholdBo FromDto(ThresholdDto thresholdDto)
    {
        return new ThresholdBo(
            thresholdDto.Id,
            new FieldBo(),
            thresholdDto.Humidity1Min,
            thresholdDto.Humidity1Max,
            thresholdDto.Humidity2Min,
            thresholdDto.Humidity2Max,
            thresholdDto.BatteryLevelMin,
            thresholdDto.BatteryLevelMax,
            thresholdDto.TemperatureMin,
            thresholdDto.TemperatureMax,
            thresholdDto.MainSensor
        );
    }

    // from entity
    public static ThresholdBo FromEntity(Threshold entity)
    {
        return new ThresholdBo(
            entity.Id,
            FieldBo.FromEntity(entity.Field),
            entity.Humidity1Min,
            entity.Humidity1Max,
            entity.Humidity2Min,
            entity.Humidity2Max,
            entity.BatteryLevelMin,
            entity.BatteryLevelMax,
            entity.TemperatureMin,
            entity.TemperatureMax,
            entity.MainSensor
        );
    }
}