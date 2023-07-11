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

    public Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets(String? name)
    {
        if (name == null)
        {
            return thresholdPresetRepository.GetGlobalThresholdPresets();
        }
        return thresholdPresetRepository.GetGlobalThresholdPresets(name);
    }

    public Task<GlobalThresholdPreset> UpdateGlobalThreshold(GlobalThresholdPresetDto globalThresholdPresetDto)
    {
        throw new NotImplementedException();
    }
}