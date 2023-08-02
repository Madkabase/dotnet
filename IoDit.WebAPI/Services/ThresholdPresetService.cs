using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class ThresholdPresetService : IThresholdPresetService
{
    private readonly IThresholdPresetRepository thresholdPresetRepository;

    public ThresholdPresetService(IThresholdPresetRepository thresholdPresetRepository)
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