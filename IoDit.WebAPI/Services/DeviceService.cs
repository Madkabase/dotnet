using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;
using IoDit.WebAPI.Utilities.Loriot;
using IoDit.WebAPI.Utilities.Loriot.Types;

namespace IoDit.WebAPI.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IFieldService _fieldService;
    private readonly LoriotApiClient _loriotApiClient;

    public DeviceService(
        IDeviceRepository deviceRepository,
        IFieldService fieldService,
        LoriotApiClient loriotApiClient
    )
    {
        _deviceRepository = deviceRepository;
        _fieldService = fieldService;
        _loriotApiClient = loriotApiClient;
    }

    public async Task<Device> CreateDevice(CreateDeviceRequestDto createDeviceRequestDto)
    {
        var field = await _fieldService.GetFieldById(createDeviceRequestDto.FieldId);
        if (field == null)
        {
            throw new Exception("Field not found");
        }

        try
        {
            await _loriotApiClient.GetLoriotAppDevice(field.Farm.AppId, createDeviceRequestDto.DevEUI);
        }
        catch (HttpRequestException e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // create device in loriot
                await _loriotApiClient.CreateLoriotAppDevice(new Utilities.Loriot.Types.LoriotCreateAppDeviceRequestDto
                {
                    deveui = createDeviceRequestDto.DevEUI,
                    appeui = createDeviceRequestDto.JoinEUI,
                    appkey = createDeviceRequestDto.AppKey,
                    title = createDeviceRequestDto.Name,
                    description = createDeviceRequestDto.Name
                }, field.Farm.AppId);
            }
            else
            {
                throw;
            }
        }
        var device = new Device
        {
            Name = createDeviceRequestDto.Name,
            Field = field,
            DevEUI = createDeviceRequestDto.DevEUI,
            AppKey = createDeviceRequestDto.AppKey,
            JoinEUI = createDeviceRequestDto.JoinEUI,
        };
        return await _deviceRepository.CreateDevice(device);
    }

}