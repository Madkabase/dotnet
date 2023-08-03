using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Persistence.Repositories;
public interface IThresholdPresetRepository
{
    Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets();
    Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets(String name);
    Task<GlobalThresholdPreset> UpdateGlobalThresholdPreset(GlobalThresholdPresetBo globalThresholdPreset);
}