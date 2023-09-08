using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;
public interface IGlobalThresholdPresetRepository
{
    Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets();
    Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets(String name);
    Task<GlobalThresholdPreset> UpdateGlobalThresholdPreset(GlobalThresholdPresetBo globalThresholdPreset);
}