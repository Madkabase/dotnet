using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Loriot;
using IoDit.WebAPI.Utilities.Loriot.Types;

namespace IoDit.WebAPI.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IFarmService _farmService;
    private readonly LoriotApiClient _loriotApiClient;
    private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    public DeviceService(
        IDeviceRepository deviceRepository,
        IFarmService farmService,
        LoriotApiClient loriotApiClient
    )
    {
        _deviceRepository = deviceRepository;
        _farmService = farmService;
        _loriotApiClient = loriotApiClient;
    }

    public async Task<DeviceBo> GetDeviceByDevEUI(string devEUI)
    {
        var device = await _deviceRepository.GetDeviceByDevEUI(devEUI) ?? throw new EntityNotFoundException("Device not found");
        return DeviceBo.FromEntity(device);
    }

    public async Task<DeviceBo> CreateDevice(FieldBo fieldBo, DeviceBo deviceBo)
    {
        FarmBo farm = await _farmService.GetFarmByFieldId(fieldBo.Id);

        // check if device already exists in loriot for this app
        try
        {
            await _loriotApiClient.GetLoriotAppDevice(farm.AppId, deviceBo.DevEUI);
        }
        catch (HttpRequestException e)
        {

            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // create device in loriot
                await _loriotApiClient.CreateLoriotAppDevice(new LoriotCreateAppDeviceRequestDto
                {
                    deveui = deviceBo.DevEUI,
                    appeui = deviceBo.JoinEUI,
                    appkey = deviceBo.AppKey,
                    title = deviceBo.Name,
                    description = deviceBo.Name
                }, farm.AppId);
            }
            else
            {
                throw;
            }
        }
        var device = new DeviceBo
        {
            JoinEUI = deviceBo.JoinEUI,
            Name = deviceBo.Name,
            DevEUI = deviceBo.DevEUI,
            AppKey = deviceBo.AppKey,
        };
        return DeviceBo.FromEntity(await _deviceRepository.CreateDevice(fieldBo, device));
    }

    public async Task DeleteDevice(string devEUI, FieldBo fieldBo)
    {
        FarmBo farm = await _farmService.GetFarmByFieldId(fieldBo.Id);
        _logger.Info($"Deleting device {devEUI} from loriot and db");
        await _deviceRepository.DeleteDevice(new DeviceBo() { DevEUI = devEUI });
        await _loriotApiClient.DeleteLoriotAppDevice(farm.AppId, devEUI);
    }

    public async Task DeleteDevicesFromField(long fieldId)
    {
        FarmBo farm = await _farmService.GetFarmByFieldId(fieldId);
        List<DeviceBo> devices = await _deviceRepository.GetDevicesFromField(fieldId).ContinueWith((el) => el.Result.Select(DeviceBo.FromEntity).ToList());
        foreach (var device in devices)
        {
            _logger.Info($"Deleting device {device.DevEUI} from loriot and db");
            await _deviceRepository.DeleteDevice(device);
            await _loriotApiClient.DeleteLoriotAppDevice(farm.AppId, device.DevEUI);
        }
    }
}