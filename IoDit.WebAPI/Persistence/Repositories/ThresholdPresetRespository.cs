using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

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

    public Task<List<ThresholdPreset>> GetThresholdPresets(long farmId)
    {
        throw new NotImplementedException();
    }
}