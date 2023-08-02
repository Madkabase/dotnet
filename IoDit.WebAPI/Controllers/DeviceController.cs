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
    public async Task<ActionResult<CreateDeviceResponseDto>> CreateDevice([FromBody] CreateDeviceRequestDto createDeviceRequestDto)
    {
        // TOOD: Check if the user has rigths to create a device
        var getDevice = await _deviceService.GetDeviceByDevEUI(createDeviceRequestDto.DevEUI);
        if (getDevice != null)
        {
            throw new BadHttpRequestException("Device already bounded to a field");
        }

        var device = await _deviceService.CreateDevice(createDeviceRequestDto);
        return Ok(new CreateDeviceResponseDto
        {
            Device = new DeviceDto
            {
                Id = device.DevEUI,
                Name = device.Name,
            },
            Message = "Device created successfully"
        });


    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public Task<User> GetRequestDetails()
    {
        throw new NotImplementedException();
    }
}