using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Services;

public interface IThresholdService
{
    public Task CreateThreshold(ThresholdDto thresholdDto, Field field);
    public Task<Threshold?> UpdateThreshold(ThresholdDto thresholdDto);
}