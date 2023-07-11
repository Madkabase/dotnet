using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Services;

public interface IThresholdPresetService
{

    /// <summary>
    /// get the list of all the global presets
    /// </summary>
    /// <param name="name">the name of the preset you want to get.
    /// Can be empty, it will retrieve all the presets.
    /// </param>
    /// <returns>the list of the global presets</returns>
    public Task<List<GlobalThresholdPreset>> GetGlobalThresholdPresets(String? name);

    /// <summary>
    /// update a preset.
    /// the user needs to be app admin to do it
    /// </summary>
    /// <param name="globalThresholdPresetDto">the global threshold preset you want to change</param>
    /// <returns>the global threshold preset</returns>  
    public Task<GlobalThresholdPreset> UpdateGlobalThreshold(GlobalThresholdPresetDto globalThresholdPresetDto);
}