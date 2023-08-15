using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.Config.Exceptions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using IoDit.WebAPI.DTO.Field;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase, IBaseController
{

    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;
    private readonly IFieldService _fieldService;
    private readonly IConfiguration _configuration;

    public DeviceController(
        IDeviceService deviceService,
        IUserService userService,
        IFieldService fieldService,
        IConfiguration configuration
    )
    {
        _deviceService = deviceService;
        _userService = userService;
        _fieldService = fieldService;
        _configuration = configuration;
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

    /// <summary>
    ///  notify all the farm admins that the field is low on water
    /// </summary>
    /// <param name="devEui">the device that is low</param>
    /// <returns></returns>
    [HttpPost("{devEui}/notifyFarmAdmins")]
    [AllowAnonymous]
    public async Task<ActionResult> NotifyFarmAdmins(string devEui, [FromHeader] string authorization)
    {
        // check the bearer token from the sender
        if (!authorization.Contains("Bearer "))
        {
            throw new UnauthorizedAccessException("Invalid token type");
        }
        authorization = authorization.Replace("Bearer ", "");
        if (authorization != _configuration["LoriotSettings-AuthorizationKey"])
        {
            throw new UnauthorizedAccessException("Invalid token");
        }

        var field = await _fieldService.GetFieldFromDeviceEui(devEui);
        // TODO: check threshold to see if above limits
        await _fieldService.NotifyFarmAdmins(field, "the field " + field.Name + "is low on water");
        return Ok();
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