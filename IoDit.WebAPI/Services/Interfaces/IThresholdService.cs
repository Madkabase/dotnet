using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Services;

public interface IThresholdService
{
    public Task<ThresholdBo> GetThresholdById(long thresholdId);
    public Task CreateThreshold(ThresholdBo thresholdDto, FieldBo field);
    public Task<ThresholdBo> UpdateThreshold(ThresholdBo thresholdDto);
    /// <summary>
    /// Deletes a threshold
    /// </summary>
    /// <param name="thresholdDto"></param>
    /// <returns></returns>
    public Task DeleteThresholdFromField(long fieldId);
}