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
        var device = await _deviceService.CreateDevice(createDeviceRequestDto);
        return Ok(device);
    }

    // ignore api methods below


    [ApiExplorerSettings(IgnoreApi = true)]
    public Task<User?> GetRequestDetails()
    {
        throw new NotImplementedException();
    }
}