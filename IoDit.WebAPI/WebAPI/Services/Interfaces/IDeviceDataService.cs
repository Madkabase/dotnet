using IoDit.WebAPI.WebAPI.Models.DeviceData;

namespace IoDit.WebAPI.WebAPI.Services.Interfaces;

public interface IDeviceDataService
{
    Task<List<DeviceDataResponseDto>?> GetDevicesData(long companyId);
    Task<List<DeviceDataResponseDto>?> GetRangedDevicesData(GetRangedDeviceDataRequestDto dto);
}