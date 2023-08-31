using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class DeviceDataService : IDeviceDataService
{
    private readonly IDeviceDataRepository _deviceDataRepository;
    public DeviceDataService(
        IDeviceDataRepository deviceDataRepository
    )
    {
        _deviceDataRepository = deviceDataRepository;
    }

    public async Task<List<DeviceDataBo>> GetDeviceDatasByDevice(DeviceBo deviceBo, DateTime startDate, DateTime endDate)
    {
        List<DeviceData> data = await _deviceDataRepository.GetDeviceDatasByDevice(deviceBo, startDate, endDate);

        return data.Select(DeviceDataBo.FromEntity).ToList();
    }
}