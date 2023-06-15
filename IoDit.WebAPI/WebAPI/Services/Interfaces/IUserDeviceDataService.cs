using IoDit.WebAPI.WebAPI.Models.UserDeviceData;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IUserDeviceDataService
{
    Task<List<UserDeviceDataResponseDto>?> GetCompanyUserThresholds(long companyId);
}