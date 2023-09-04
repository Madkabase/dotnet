using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IThresholdPresetRespository
{
    Task<List<ThresholdPreset>> GetThresholdPresets(long farmId);
    Task<ThresholdPreset> CreateThresholdPreset(ThresholdPresetBo thresholdPreset);
}