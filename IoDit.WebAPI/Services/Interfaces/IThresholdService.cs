using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Services;

public interface IThresholdService
{
    public Task CreateThreshold(ThresholdBo thresholdDto, FieldBo field);
    public Task<ThresholdBo> UpdateThreshold(ThresholdBo thresholdDto);
}