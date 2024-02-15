using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class ThresholdBo
{
    public long Id { get; set; }
    // public FieldBo Field { get; set; }
    public int Moisture1Min { get; set; }
    public int Moisture1Max { get; set; }
    public int Moisture2Min { get; set; }
    public int Moisture2Max { get; set; }
    public float Temperature1Min { get; set; }
    public float Temperature1Max { get; set; }
    public float Temperature2Min { get; set; }
    public float Temperature2Max { get; set; }
    public float Salinity1Min { get; set; }
    public float Salinity1Max { get; set; }
    public float Salinity2Min { get; set; }
    public float Salinity2Max { get; set; }
    public MainSensor MainSensor { get; set; }

    public ThresholdBo()
    {
        // Field = new FieldBo();
        MainSensor = MainSensor.SensorDown;
        Moisture1Min = 0;
        Moisture1Max = 0;
        Moisture2Min = 0;
        Moisture2Max = 0;
        Temperature1Min = 0;
        Temperature1Max = 0;
        Temperature2Min = 0;
        Temperature2Max = 0;
        Salinity1Min = 0;
        Salinity1Max = 0;
        Salinity2Min = 0;
        Salinity2Max = 0;
    }
    public ThresholdBo(
        long id,
        int humidity1Min,
        int humidity1Max,
        int humidity2Min,
        int humidity2Max,
        float temperature1Min,
        float temperature1Max,
        float temperature2Min,
        float temperature2Max,
        float salinity1Min,
        float salinity1Max,
        float salinity2Min,
        float salinity2Max,
        MainSensor mainSensor
        )
    {
        Id = id;
        Moisture1Min = humidity1Min;
        Moisture1Max = humidity1Max;
        Moisture2Min = humidity2Min;
        Moisture2Max = humidity2Max;

        Temperature1Min = temperature1Min;
        Temperature1Max = temperature1Max;
        Temperature2Min = temperature2Min;
        Temperature2Max = temperature2Max;
        
        Salinity1Min = salinity1Min;
        Salinity1Max = salinity1Max;
        Salinity2Min = salinity2Min;
        Salinity2Max = salinity2Max;

        MainSensor = mainSensor;
    }

    // from dto
    public static ThresholdBo FromDto(ThresholdDto thresholdDto)
    {
        return new ThresholdBo(
            thresholdDto.Id,
            // new FieldBo(),
            thresholdDto.Moisture1Min,
            thresholdDto.Moisture1Max,
            thresholdDto.Moisture2Min,
            thresholdDto.Moisture2Max,
            thresholdDto.Temperature1Min,
            thresholdDto.Temperature1Max,
            thresholdDto.Temperature2Min,
            thresholdDto.Temperature2Max,
            thresholdDto.Salinity1Min,
            thresholdDto.Salinity1Max,
            thresholdDto.Salinity2Min,
            thresholdDto.Salinity2Max,
            thresholdDto.MainSensor
        );
    }

    // from entity
    public static ThresholdBo FromEntity(Threshold entity)
    {
        return new ThresholdBo(
            entity.Id,
            // FieldBo.FromEntity(entity.Field),
            entity.Moisture1Min,
            entity.Moisture1Max,
            entity.Moisture2Min,
            entity.Moisture2Max,
            entity.Temperature1Min,
            entity.Temperature1Max,
            entity.Temperature2Min,
            entity.Temperature2Max,
            entity.Salinity1Min,
            entity.Salinity1Max,
            entity.Salinity2Min,
            entity.Salinity2Max,
            entity.MainSensor
        );
    }
}