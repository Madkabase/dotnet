using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IThresholdPresetRespository
{
    /// <summary>
    /// get the threshold presets from a farm
    /// </summary>
    /// <param name="farmId"></param>
    /// <returns></returns>
    Task<List<ThresholdPreset>> GetThresholdPresets(long farmId);
    /// <summary>
    /// Create a threshold preset for a farms
    /// </summary>
    /// <param name="thresholdPreset"></param>
    /// <returns></returns>
    Task<ThresholdPreset> CreateThresholdPreset(ThresholdPresetBo thresholdPreset);
    /// <summary>
    /// delete a threshold preset
    /// </summary>
    /// <param name="thresholdPresetId"></param>
    /// <returns></returns>
    Task DeleteThresholdPreset(long thresholdPresetId);
    /// <summary>
    /// update a threshold preset
    /// </summary>
    /// <param name="thresholdPreset"></param>
    /// <returns></returns>
    Task UpdateThresholdPreset(ThresholdPresetBo thresholdPreset);
}