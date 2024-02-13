using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class ThresholdBo
{
    public long Id { get; set; }
    // public FieldBo Field { get; set; }
    public int Humidity1Min { get; set; }
    public int Humidity1Max { get; set; }
    public int Humidity2Min { get; set; }
    public int Humidity2Max { get; set; }
    public double Temperature1Min { get; set; }
    public double Temperature1Max { get; set; }
    public double Temperature2Min { get; set; }
    public double Temperature2Max { get; set; }
    public MainSensor MainSensor { get; set; }

    public ThresholdBo()
    {
        // Field = new FieldBo();
        MainSensor = MainSensor.SensorDown;
        Humidity1Min = 0;
        Humidity1Max = 0;
        Humidity2Min = 0;
        Humidity2Max = 0;
        Temperature1Min = 0;
        Temperature1Max = 0;
        Temperature2Min = 0;
        Temperature2Max = 0;
    }
    public ThresholdBo(long id,/* FieldBo field,*/ int humidity1Min, int humidity1Max, int humidity2Min, int humidity2Max, double temperature1Min, double temperature1Max, double temperature2Min, double temperature2Max, MainSensor mainSensor)
    {
        Id = id;
        // Field = field;
        Humidity1Min = humidity1Min;
        Humidity1Max = humidity1Max;
        Humidity2Min = humidity2Min;
        Humidity2Max = humidity2Max;

        Temperature1Min = temperature1Min;
        Temperature1Max = temperature1Max;
        Temperature2Min = temperature2Min;
        Temperature2Max = temperature2Max;
        MainSensor = mainSensor;
    }

    // from dto
    public static ThresholdBo FromDto(ThresholdDto thresholdDto)
    {
        return new ThresholdBo(
            thresholdDto.Id,
            // new FieldBo(),
            thresholdDto.Humidity1Min,
            thresholdDto.Humidity1Max,
            thresholdDto.Humidity2Min,
            thresholdDto.Humidity2Max,
            thresholdDto.Temperature1Min,
            thresholdDto.Temperature1Max,
            thresholdDto.Temperature2Min,
            thresholdDto.Temperature2Max,
            thresholdDto.MainSensor
        );
    }

    // from entity
    public static ThresholdBo FromEntity(Threshold entity)
    {
        return new ThresholdBo(
            entity.Id,
            // FieldBo.FromEntity(entity.Field),
            entity.Humidity1Min,
            entity.Humidity1Max,
            entity.Humidity2Min,
            entity.Humidity2Max,
            entity.Temperature1Min,
            entity.Temperature1Max,
            entity.Temperature2Min,
            entity.Temperature2Max,
            entity.MainSensor
        );
    }
}