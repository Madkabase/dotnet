
using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;
public class ThresholdService : IThresholdService
{

    private readonly IThresholdRepository _thresholdRepository;

    public ThresholdService(IThresholdRepository thresholdRepository)
    {
        this._thresholdRepository = thresholdRepository;
    }

    public async Task CreateThreshold(ThresholdBo thresholdBo, FieldBo field)
    {
        var threshold = Threshold.FromBo(thresholdBo);
        threshold.Field = Field.FromBo(field);
        await _thresholdRepository.CreateThreshold(thresholdBo);
    }

    public async Task<ThresholdBo> UpdateThreshold(ThresholdBo thresholdBo)
    {
        var newThreshold = await _thresholdRepository.UpdateThreshold(thresholdBo)
            ?? throw new Exception();
        return ThresholdBo.FromEntity(newThreshold);
    }


}