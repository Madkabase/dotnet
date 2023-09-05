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
}