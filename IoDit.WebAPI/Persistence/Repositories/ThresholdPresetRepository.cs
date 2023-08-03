using IoDit.WebAPI.BO;
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
        return await Task.Run(() => _dbContext.GlobalThresholdPresets.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList());
    }

    public Task<GlobalThresholdPreset> UpdateGlobalThresholdPreset(GlobalThresholdPresetBo globalThresholdPresetDto)
    {
        //TODO: Implement
        throw new NotImplementedException();
    }
}