using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase, IBaseController
{

    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost]
    public async Task<ActionResult<DeviceDto>> CreateDevice([FromBody] CreateDeviceRequestDto createDeviceRequestDto)
    {
        // TODO: Check if the user has rigths to create a device
        var getDevice = await _deviceService.GetDeviceByDevEUI(createDeviceRequestDto.DevEUI);
        if (getDevice != null)
        {
            throw new BadHttpRequestException("Device already bounded to a field");
        }

        DeviceBo deviceBo = new DeviceBo
        {
            DevEUI = createDeviceRequestDto.DevEUI,
            Name = createDeviceRequestDto.Name,
            AppKey = createDeviceRequestDto.AppKey,
            JoinEUI = createDeviceRequestDto.JoinEUI,
        };


        var device = await _deviceService.CreateDevice(new FieldBo { Id = createDeviceRequestDto.FieldId }, deviceBo);
        return Ok(DeviceDto.FromBo(device));


    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public Task<UserBo> GetRequestDetails()
    {
        throw new NotImplementedException();
    }
}