using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class GlobalThresholdPresetService : IGlobalThresholdPresetService
{
    private readonly IGlobalThresholdPresetRepository thresholdPresetRepository;

    public GlobalThresholdPresetService(IGlobalThresholdPresetRepository thresholdPresetRepository)
    {
        this.thresholdPresetRepository = thresholdPresetRepository;
    }

    public async Task<List<GlobalThresholdPresetBo>> GetGlobalThresholdPresets(String? name)
    {
        if (name == null)
        {
            return (await thresholdPresetRepository.GetGlobalThresholdPresets()).Select(x => GlobalThresholdPresetBo.FromEntity(x)).ToList();
        }
        return (await thresholdPresetRepository.GetGlobalThresholdPresets(name)).Select(x => GlobalThresholdPresetBo.FromEntity(x)).ToList();

    }

    public Task<GlobalThresholdPresetBo> UpdateGlobalThreshold(GlobalThresholdPresetBo globalThresholdPresetDto)
    {
        throw new NotImplementedException();
    }
}