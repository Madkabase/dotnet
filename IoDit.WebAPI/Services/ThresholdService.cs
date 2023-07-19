
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;
public class ThresholdService : IThresholdService
{

    private readonly IThresholdRepository thresholdRepository;

    public ThresholdService(IThresholdRepository thresholdRepository)
    {
        this.thresholdRepository = thresholdRepository;
    }

    public async Task CreateThreshold(ThresholdDto thresholdDto, Field field)
    {
        var threshold = Threshold.FromDto(thresholdDto);
        threshold.Field = field;
        await thresholdRepository.CreateThreshold(threshold);
    }

    public async Task<Threshold?> UpdateThreshold(ThresholdDto thresholdDto)
    {
        var threshold = Threshold.FromDto(thresholdDto);
        return await thresholdRepository.UpdateThreshold(threshold);
    }


}