using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence.Repositories;

public class ThresholdPresetRespository : IThresholdPresetRespository

{
    private readonly AgroditDbContext _dbContext;

    public ThresholdPresetRespository(AgroditDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ThresholdPreset> CreateThresholdPreset(ThresholdPresetBo thresholdPreset)
    {
        var newPreset = _dbContext.ThresholdPresets.Add(ThresholdPreset.FromBo(thresholdPreset));

        await _dbContext.SaveChangesAsync();

        return newPreset.Entity;
    }

    public async Task<List<ThresholdPreset>> GetThresholdPresets(long farmId)
    {
        return await _dbContext.ThresholdPresets.Where(t => t.FarmId == farmId).ToListAsync();

    }

    public async Task DeleteThresholdPreset(long thresholdPresetId)
    {
        _dbContext.ThresholdPresets.Remove(new()
        {
            Id = thresholdPresetId
        });
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateThresholdPreset(ThresholdPresetBo thresholdPreset)
    {
        ThresholdPreset preset = _dbContext.ThresholdPresets.Find(thresholdPreset.Id) ?? throw new ArgumentException("Threshold preset not found");
        preset.Name = thresholdPreset.Name;
        preset.Humidity1Min = thresholdPreset.Humidity1Min;
        preset.Humidity1Max = thresholdPreset.Humidity1Max;
        preset.Humidity2Min = thresholdPreset.Humidity2Min;
        preset.Humidity2Max = thresholdPreset.Humidity2Max;
        preset.TemperatureMin = thresholdPreset.TemperatureMin;
        preset.TemperatureMax = thresholdPreset.TemperatureMax;
        preset.BatteryLevelMin = thresholdPreset.BatteryLevelMin;
        preset.BatteryLevelMax = thresholdPreset.BatteryLevelMax;


        _dbContext.ThresholdPresets.Update(preset);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<ThresholdPreset>> GetThresholdPresetsByName(long farmId, string name)
    {
        return await _dbContext.ThresholdPresets.Where(t => t.FarmId == farmId)
        .Where(t => t.Name.Contains(name))
        .ToListAsync();
    }
}