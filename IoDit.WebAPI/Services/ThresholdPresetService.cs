using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Repositories;
using Microsoft.Extensions.Azure;

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

    public async Task<List<ThresholdPresetBo>> GetThresholdPresets(long farmId)
    {
        return await _thresholdPresetRespository.GetThresholdPresets(farmId).ContinueWith(t => t.Result.Select(ThresholdPresetBo.FromEntity).ToList());
    }
    public async Task DeleteThresholdPreset(long thresholdPresetId)
    {
        await _thresholdPresetRespository.DeleteThresholdPreset(thresholdPresetId);
    }
}