using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;


public class ThresholdRepository : IThresholdRepository
{
    private readonly AgroditDbContext context;

    public ThresholdRepository(AgroditDbContext context)
    {
        this.context = context;
    }

    public async Task CreateThreshold(Threshold threshold)
    {
        await context.AddAsync<Threshold>(threshold);
        await context.SaveChangesAsync();
    }

    public async Task<Threshold?> UpdateThreshold(Threshold threshold)
    {
        var uThreshold = context.Thresholds.Find(threshold.Id);
        if (threshold == null)
        {
            return null;
        }
        uThreshold.BatteryLevelMax = threshold.BatteryLevelMax;
        uThreshold.BatteryLevelMin = threshold.BatteryLevelMin;
        uThreshold.Humidity1Max = threshold.Humidity1Max;
        uThreshold.Humidity1Min = threshold.Humidity1Min;
        uThreshold.Humidity2Max = threshold.Humidity2Max;
        uThreshold.Humidity2Min = threshold.Humidity2Min;
        uThreshold.TemperatureMax = threshold.TemperatureMax;
        uThreshold.TemperatureMin = threshold.TemperatureMin;
        uThreshold.MainSensor = threshold.MainSensor;

        await context.SaveChangesAsync();
        return threshold;
    }
}