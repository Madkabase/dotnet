using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.DeviceData;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class DeviceDataService : IDeviceDataService
{
    private readonly IIoDitRepository _repository;
    private readonly IDeviceRepository _deviceRepository;


    public DeviceDataService(IIoDitRepository repository,
        IDeviceRepository deviceRepository)
    {
        _repository = repository;
        _deviceRepository = deviceRepository;
    }

    public async Task<List<DeviceDataResponseDto>?> GetDevicesData(long companyId)
    {
        var devicesData = (await _deviceRepository.GetDevices(companyId)).SelectMany(x => x.DeviceData).ToList();
        if (!devicesData.Any())
        {
            return new List<DeviceDataResponseDto>();
        }

        return devicesData.Select(x => new DeviceDataResponseDto()
        {
            Id = x.Id,
            Sensor1 = x.Sensor1,
            Sensor2 = x.Sensor2,
            Temperature = x.Temperature,
            BatteryLevel = x.BatteryLevel,
            DeviceId = x.DeviceId,
            TimeStamp = x.TimeStamp
        }).ToList();
    }

    public async Task<List<DeviceDataResponseDto>?> GetRangedDevicesData(GetRangedDeviceDataRequestDto dto)
    {
        var device = await _deviceRepository.GetDeviceWithDataByEui(dto.DevEui);
        if (device == null) return new List<DeviceDataResponseDto>();

        var deviceData = device.DeviceData.Where(x => x.TimeStamp >= dto.Start && x.TimeStamp <= dto.End).ToList();

        return deviceData.Select(x => new DeviceDataResponseDto()
        {
            Id = x.Id,
            Sensor1 = x.Sensor1,
            Sensor2 = x.Sensor2,
            Temperature = x.Temperature,
            BatteryLevel = x.BatteryLevel,
            DeviceId = x.DeviceId,
            TimeStamp = x.TimeStamp
        }).ToList();
    }
}