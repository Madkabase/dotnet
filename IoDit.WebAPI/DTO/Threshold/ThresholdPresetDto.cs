using IoDit.WebAPI.DTO.Farm;

namespace IoDit.WebAPI.DTO.Threshold
{
    public class ThresholdPresetDto : ThresholdDto
    {
        String Name { get; set; }
        FarmDTO Farm { get; set; }


        internal static ThresholdPresetDto FromEntity(Persistence.Entities.ThresholdPreset thresholdPreset)
        {
            return new ThresholdPresetDto
            {
                Id = thresholdPreset.Id,
                Name = thresholdPreset.Name,
                Humidity1Min = thresholdPreset.Humidity1Min,
                Humidity1Max = thresholdPreset.Humidity1Max,
                Humidity2Min = thresholdPreset.Humidity2Min,
                Humidity2Max = thresholdPreset.Humidity2Max,
                TemperatureMin = thresholdPreset.TemperatureMin,
                TemperatureMax = thresholdPreset.TemperatureMax,
                BatteryLevelMin = thresholdPreset.BatteryLevelMin,
                BatteryLevelMax = thresholdPreset.BatteryLevelMax
            };
        }
    }
}