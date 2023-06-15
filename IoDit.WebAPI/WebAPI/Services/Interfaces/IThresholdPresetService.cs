using IoDit.WebAPI.WebAPI.Models.ThresholdPreset;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IThresholdPresetService
{
    Task<List<GetThresholdPresetsResponseDto>?> GetThresholdPresets(long companyId);
    Task<GetThresholdPresetsResponseDto> CreateThresholdPreset(CreateThresholdPreset request);
}