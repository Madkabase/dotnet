using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;


public class ThresholdRepository : IThresholdRepository
{
    private readonly AgroditDbContext context;

    public ThresholdRepository(AgroditDbContext context)
    {
        this.context = context;
    }

    public async Task CreateThreshold(Threshold threshold)
    {
        await context.AddAsync<Threshold>(threshold);
        await context.SaveChangesAsync();
    }
}