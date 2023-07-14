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
    public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceRequestDto createDeviceRequestDto)
    {
        try
        {
            var device = await _deviceService.CreateDevice(createDeviceRequestDto);
            return Ok(new CreateDeviceResponseDto
            {
                Device = new DeviceDto
                {
                    Id = device.Id,
                    Name = device.Name,
                },
                Message = "Device created successfully"
            });
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public Task<User?> GetRequestDetails()
    {
        throw new NotImplementedException();
    }
}