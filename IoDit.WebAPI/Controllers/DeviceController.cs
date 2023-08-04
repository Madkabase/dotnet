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

    public DeviceController(
        IDeviceService deviceService,
        IUserService userService,
        IFieldService fieldService
    )
    {
        _deviceService = deviceService;
        _userService = userService;
        _fieldService = fieldService;
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