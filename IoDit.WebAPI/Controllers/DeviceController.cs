using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.Config.Exceptions;

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
        try
        {
            var getDevice = await _deviceService.GetDeviceByDevEUI(createDeviceRequestDto.DevEUI);
            throw new BadHttpRequestException("Device already bounded to a field");
        }
        catch (EntityNotFoundException)
        {
            DeviceBo deviceBo = new()
            {
                DevEUI = createDeviceRequestDto.DevEUI,
                Name = createDeviceRequestDto.Name,
                AppKey = createDeviceRequestDto.AppKey,
                JoinEUI = createDeviceRequestDto.JoinEUI,
            };

            var device = await _deviceService.CreateDevice(new FieldBo { Id = createDeviceRequestDto.FieldId }, deviceBo);
            return Ok(DeviceDto.FromBo(device));
        }
        catch (BadHttpRequestException) { throw; }
        catch (System.Exception) { throw; }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public Task<UserBo> GetRequestDetails()
    {
        throw new NotImplementedException();
    }
}