using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
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

    public async Task CreateThreshold(ThresholdBo threshold)
    {
        await context.AddAsync<Threshold>(Threshold.FromBo(threshold));
        await context.SaveChangesAsync();
    }

    public async Task<Threshold?> UpdateThreshold(ThresholdBo threshold)
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

        context.Thresholds.Update(uThreshold);
        await context.SaveChangesAsync();
        return uThreshold;
    }

    public Task<Threshold?> GetThresholdById(long id)
    {
        return context.Thresholds.FindAsync(id).AsTask();
    }

    public Task DeleteThresholdFromField(long fieldId)
    {
        // find field by id, knowing that the id field name is "Id"

        var field = context.Fields.Find(fieldId);
        if (field == null) throw new EntityNotFoundException("Field not found");
        context.Thresholds.Remove(new() { Id = field.ThresholdId });
        return context.SaveChangesAsync();
    }
}