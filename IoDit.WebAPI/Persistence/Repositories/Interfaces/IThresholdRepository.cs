using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;

public interface IThresholdRepository
{
    public Task CreateThreshold(ThresholdBo threshold);
    public Task<Threshold?> UpdateThreshold(ThresholdBo threshold);
}
