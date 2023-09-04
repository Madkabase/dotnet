using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IGlobalThresholdPresetService
{
    /// <summary>
    /// get the list of all the global presets
    /// </summary>
    /// <param name="name">the name of the preset you want to get.
    /// Can be empty, it will retrieve all the presets.
    /// </param>
    /// <returns>the list of the global presets</returns>
    public Task<List<GlobalThresholdPresetBo>> GetGlobalThresholdPresets(String? name);

    /// <summary>
    /// update a preset.
    /// the user needs to be app admin to do it
    /// </summary>
    /// <param name="globalThresholdPresetDto">the global threshold preset you want to change</param>
    /// <returns>the global threshold preset</returns>  
    public Task<GlobalThresholdPresetBo> UpdateGlobalThreshold(GlobalThresholdPresetBo globalThresholdPresetDto);
}