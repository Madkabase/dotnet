using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class ThresholdPresetService : IThresholdPresetService
{

    private readonly IThresholdPresetRespository _thresholdPresetRespository;

    public ThresholdPresetService(IThresholdPresetRespository thresholdPresetRespository)
    {
        _thresholdPresetRespository = thresholdPresetRespository;
    }

    public async Task<ThresholdPresetBo> CreateThresholdPreset(long farmId, ThresholdPresetBo thresholdBo)
    {
        thresholdBo.Farm.Id = farmId;
        return ThresholdPresetBo.FromEntity(await _thresholdPresetRespository.CreateThresholdPreset(thresholdBo));
    }

}