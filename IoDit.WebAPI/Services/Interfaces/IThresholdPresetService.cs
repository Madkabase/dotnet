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

}