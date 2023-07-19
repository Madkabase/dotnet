using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IThresholdRepository
{
    public Task CreateThreshold(Threshold thresholdDto);
    public Task<Threshold?> UpdateThreshold(Threshold thresholdDto);
}
