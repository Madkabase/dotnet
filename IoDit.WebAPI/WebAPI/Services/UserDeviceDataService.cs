using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.UserDeviceData;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class UserDeviceDataService: IUserDeviceDataService
{
    private readonly IIoDitRepository _repository;

    public UserDeviceDataService(IIoDitRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<UserDeviceDataResponseDto>?> GetCompanyUserThresholds(long companyUserId)
    {
        var userThresholds = await _repository.GetCompanyUserThresholds(companyUserId);
        if (!userThresholds.Any())
        {
            return new List<UserDeviceDataResponseDto>();
        }

        return userThresholds.Select(x => new UserDeviceDataResponseDto()
        {
            Id = x.Id,
            DeviceId = x.DeviceId,
            Humidity1Max = x.Humidity1Max,
            Humidity1Min = x.Humidity1Min,
            Humidity2Max = x.Humidity2Max,
            Humidity2Min = x.Humidity2Min,
            TemperatureMax = x.TemperatureMax,
            TemperatureMin = x.TemperatureMin,
            BatteryLevelMax = x.BatteryLevelMax,
            BatteryLevelMin = x.BatteryLevelMin,
            UserId = x.UserId//companyUserId
        }).ToList();
    }
}