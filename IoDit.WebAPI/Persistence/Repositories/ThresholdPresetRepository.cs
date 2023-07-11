using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public class ThresholdPresetRepository : IThresholdPresetRepository
{

    private readonly AgroditDbContext _dbContext;

    public ThresholdPresetRepository(AgroditDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets()
    {
        return await Task.Run(() => _dbContext.GlobalThresholdPresets.ToList());
    }

    public async Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets(String name)
    {
        return await Task.Run(() => _dbContext.GlobalThresholdPresets.Where(x => x.Name.ToUpperInvariant().Contains(name.ToUpperInvariant())).ToList());
    }

    public Task<GlobalThresholdPreset> UpdateGlobalThresholdPreset(GlobalThresholdPreset globalThresholdPresetDto)
    {
        throw new NotImplementedException();
    }
}