using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IThresholdPresetService
{
    /// <summary>
    /// Create a threshold preset for a farm
    /// </summary>
    /// <param name="farmId"></param>
    /// <param name="thresholdBo"></param>
    /// <returns></returns>
    Task<ThresholdPresetBo> CreateThresholdPreset(long farmId, ThresholdPresetBo thresholdBo);

    /// <summary>
    /// get the threshold presets from a farm
    /// </summary>
    /// <param name="farmId"></param>
    /// <returns></returns>
    Task<List<ThresholdPresetBo>> GetThresholdPresets(long farmId);
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
    /// <summary>
    /// get the threshold presets from a farm, filtered by name
    /// </summary>
    /// <param name="farmId">the id of the farm we want </param>
    /// <param name="name">filtered name</param>
    /// <returns></returns>
    Task<IEnumerable<ThresholdPresetBo>> GetThresholdPresetsByName(long farmId, string name);
}