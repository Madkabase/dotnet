using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.Config.Exceptions;
using System.Security.Claims;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase, IBaseController
{

    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;
    private readonly IFieldService _fieldService;
    private readonly IDeviceDataService _deviceDataService;

    public DeviceController(
        IDeviceService deviceService,
        IUserService userService,
        IFieldService fieldService,
        IDeviceDataService deviceDataService
    )
    {
        _deviceService = deviceService;
        _userService = userService;
        _fieldService = fieldService;
        _deviceDataService = deviceDataService;
    }

    [HttpPost]
    public async Task<ActionResult<DeviceDto>> CreateDevice([FromBody] CreateDeviceRequestDto createDeviceRequestDto)
    {
        // TODO: Check if the user has rigths to create a device
        bool canChangeField = await _fieldService.UserCanChangeField(createDeviceRequestDto.FieldId, await GetRequestDetails());
        if (!canChangeField)
        {
            throw new UnauthorizedAccessException("User does not have rights to change this field");
        }
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
        catch (Exception) { throw; }
    }

    [HttpGet("{devEUI}/data")]
    public async Task<ActionResult<List<DeviceDataDTO>>> GetDeviceData([FromRoute] string devEUI, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        // TODO: Check if the user has rigths to access device data from field

        if (endDate == null)
        {
            endDate = DateTime.Now;
        }
        if (startDate == null)
        {
            startDate = endDate?.AddDays(-1);
        }

        var device = await _deviceService.GetDeviceByDevEUI(devEUI);
        var deviceData = await _deviceDataService.GetDeviceDatasByDevice(device, (DateTime)startDate!, (DateTime)endDate!);
        return Ok(deviceData.Select(DeviceDataDTO.FromBo).ToList());
    }


    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<UserBo> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = (userIdClaim?.Value) ?? throw new UnauthorizedAccessException("Invalid user");
        var user = await _userService.GetUserByEmail(userId);
        return user;
        throw new NotImplementedException();
    }
}