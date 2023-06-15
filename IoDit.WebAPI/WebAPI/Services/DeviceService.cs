using IoDit.WebAPI.Persistence.Entities.Company;
using IoDit.WebAPI.Utilities.Azure;
using IoDit.WebAPI.Utilities.Loriot;
using IoDit.WebAPI.Utilities.Loriot.Types;
using IoDit.WebAPI.Utilities.Repositories;
using IoDit.WebAPI.WebAPI.Models.Device;
using IoDit.WebAPI.WebAPI.Models.DeviceData;
using IoDit.WebAPI.WebAPI.Services.Interfaces;

namespace IoDit.WebAPI.WebAPI.Services;

public class DeviceService : IDeviceService
{
    private readonly IIoDitRepository _repository;
    private readonly LoriotApiClient _loriotApiClient;
    private readonly IAzureApiClient _azureApiClient;
    private readonly ICompanyRepository _companyRepository;


    public DeviceService(
        IIoDitRepository repository,
        LoriotApiClient loriotApiClient,
        IAzureApiClient azureApiClient,
        ICompanyRepository companyRepository
    )
    {
        _repository = repository;
        _loriotApiClient = loriotApiClient;
        _azureApiClient = azureApiClient;
        _companyRepository = companyRepository;
    }

    public async Task<GetDevicesResponseDto> CreateDevice(CreateDeviceRequestDto request)
    {
        var company = await _companyRepository.GetCompanyById(request.CompanyId);
        if (company == null)
        {
            throw new Exception("Company doesnt exist");
        }

        var device = await _repository.GetDeviceByEui(request.DeviceEUI);
        if (device != null)
        {
            throw new Exception("Device already exist");
        }

        var farm = await _repository.GetCompanyFarmById(request.FarmId);
        if (farm == null)
        {
            throw new Exception("Farm doesnt exist");
        }

        var loriotDevice = await _loriotApiClient.CreateLoriotAppDevice(new LoriotCreateAppDeviceRequestDto()
        {
            appkey = request.AppKey,
            description = "device created by IoDit.WebAPI",
            deveui = request.DeviceEUI,
            title = request.DeviceName,
            appeui = request.JoinEUI
        }, company.AppId);

        var azureDevice = await _azureApiClient.CreateDevice(loriotDevice.deveui);

        var createdDevice = await _repository.CreateAsync(new CompanyDevice()
        {
            Farm = farm,
            FarmId = farm.Id,
            Company = company,
            CompanyId = company.Id,
            Name = loriotDevice.title,
            AppKey = request.AppKey,
            DevEUI = loriotDevice.deveui,
            JoinEUI = request.JoinEUI,
            DefaultHumidity1Max = request.DefaultHumidity1Max,
            DefaultHumidity2Max = request.DefaultHumidity2Max,
            DefaultHumidity1Min = request.DefaultHumidity1Min,
            DefaultHumidity2Min = request.DefaultHumidity2Min,
            DefaultTemperatureMax = request.DefaultTemperatureMax,
            DefaultTemperatureMin = request.DefaultTemperatureMin,
            DefaultBatteryLevelMax = request.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = request.DefaultBatteryLevelMin
        });

        return new GetDevicesResponseDto()
        {
            Id = createdDevice.Id,
            Name = createdDevice.Name,
            AppKey = createdDevice.AppKey,
            CompanyId = createdDevice.CompanyId,
            DeviceData = createdDevice.DeviceData.Select(x => new DeviceDataResponseDto()
            {
                Id = x.Id,
                Sensor1 = x.Sensor1,
                Sensor2 = x.Sensor2,
                Temperature = x.Temperature,
                BatteryLevel = x.BatteryLevel,
                DeviceId = x.DeviceId,
                TimeStamp = x.TimeStamp
            }).ToList(),
            FarmId = createdDevice.FarmId,
            FieldId = createdDevice.FieldId,
            DefaultHumidity1Max = createdDevice.DefaultHumidity1Max,
            DefaultHumidity1Min = createdDevice.DefaultHumidity1Min,
            DefaultHumidity2Max = createdDevice.DefaultHumidity2Max,
            DefaultHumidity2Min = createdDevice.DefaultHumidity2Min,
            DefaultTemperatureMax = createdDevice.DefaultTemperatureMax,
            DefaultTemperatureMin = createdDevice.DefaultTemperatureMin,
            DefaultBatteryLevelMax = createdDevice.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = createdDevice.DefaultBatteryLevelMin,
            DevEUI = createdDevice.DevEUI,
            JoinEUI = createdDevice.JoinEUI
        };
    }

    public async Task<List<GetDevicesResponseDto>?> GetDevices(long companyId)
    {
        var devices = await _repository.GetDevices(companyId);
        if (!devices.Any())
        {
            return new List<GetDevicesResponseDto>();
        }

        return devices.Select(device => new GetDevicesResponseDto()
        {
            Id = device.Id,
            Name = device.Name,
            AppKey = device.AppKey,
            CompanyId = device.CompanyId,
            DeviceData = device.DeviceData?.Select(x => new DeviceDataResponseDto()
            {
                Sensor1 = x.Sensor1,
                Sensor2 = x.Sensor2,
                Temperature = x.Temperature,
                BatteryLevel = x.BatteryLevel,
                DeviceId = x.DeviceId,
                TimeStamp = x.TimeStamp,
                Id = x.Id
            }).ToList() ?? new List<DeviceDataResponseDto>(),
            FarmId = device.FarmId,
            FieldId = device.FieldId,
            DefaultHumidity1Max = device.DefaultHumidity1Max,
            DefaultHumidity1Min = device.DefaultHumidity1Min,
            DefaultHumidity2Max = device.DefaultHumidity2Max,
            DefaultHumidity2Min = device.DefaultHumidity2Min,
            DefaultTemperatureMax = device.DefaultTemperatureMax,
            DefaultTemperatureMin = device.DefaultTemperatureMin,
            DefaultBatteryLevelMax = device.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = device.DefaultBatteryLevelMin,
            DevEUI = device.DevEUI,
            JoinEUI = device.JoinEUI
        }).ToList();
    }

    public async Task<GetDevicesResponseDto?> AssignToField(AssignToFieldRequestDto dto)
    {
        var device = await _repository.GetDeviceByEui(dto.DeviceEui);
        if (device == null)
        {
            return null;
        }

        var field = await _repository.GetCompanyFieldById(dto.FieldId);
        if (field == null)
        {
            return null;
        }

        device.Field = field;
        device.FieldId = field.Id;
        var updated = await _repository.UpdateAsync(device);

        return new GetDevicesResponseDto()
        {
            Id = updated.Id,
            Name = updated.Name,
            AppKey = updated.AppKey,
            CompanyId = updated.CompanyId,
            DeviceData = updated.DeviceData?.Select(x => new DeviceDataResponseDto()
            {
                Sensor1 = x.Sensor1,
                Sensor2 = x.Sensor2,
                Temperature = x.Temperature,
                BatteryLevel = x.BatteryLevel,
                DeviceId = x.DeviceId,
                TimeStamp = x.TimeStamp,
                Id = x.Id
            }).ToList() ?? new List<DeviceDataResponseDto>(),
            FarmId = updated.FarmId,
            FieldId = updated.FieldId,
            DefaultHumidity1Max = updated.DefaultHumidity1Max,
            DefaultHumidity1Min = updated.DefaultHumidity1Min,
            DefaultHumidity2Max = updated.DefaultHumidity2Max,
            DefaultHumidity2Min = updated.DefaultHumidity2Min,
            DefaultTemperatureMax = updated.DefaultTemperatureMax,
            DefaultTemperatureMin = updated.DefaultTemperatureMin,
            DefaultBatteryLevelMax = updated.DefaultBatteryLevelMax,
            DefaultBatteryLevelMin = updated.DefaultBatteryLevelMin,
            DevEUI = updated.DevEUI,
            JoinEUI = updated.JoinEUI
        };
    }
}