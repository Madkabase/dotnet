using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using IoDit.WebAPI.Config.Exceptions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Migrations;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceController : ControllerBase, IBaseController
{

    private readonly IDeviceService _deviceService;
    private readonly IUserService _userService;
    private readonly IFieldService _fieldService;
    private readonly IDeviceDataService _deviceDataService;
    private readonly IConfiguration _configuration;
    private readonly IThresholdService _thresholdService;
    private readonly IAlertService _alertService;
    private readonly NLog.ILogger _logger;

    public DeviceController(
        IDeviceService deviceService,
        IUserService userService,
        IFieldService fieldService,
        IDeviceDataService deviceDataService,
        IConfiguration configuration,
        IThresholdService thresholdService,
        IAlertService alertService
    )
    {
        _deviceService = deviceService;
        _userService = userService;
        _fieldService = fieldService;
        _deviceDataService = deviceDataService;
        _configuration = configuration;
        _thresholdService = thresholdService;
        _alertService = alertService;
        _logger = NLog.LogManager.GetCurrentClassLogger();
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


    /// <summary>
    ///  notify all the farm admins that the field is low on water
    ///  triggered when a new device data is added by the cloud function
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

        // if the field already has an active alert return Ok()
        if (await _alertService.hasActiveAlert(field))
        {
            _logger.Info("Field already has an active alert");
            return Ok();
        }

        var threshold = await _thresholdService.GetThresholdById((long)field.ThresholdId!);
        var device = await _deviceService.GetDeviceByDevEUI(devEui);
        string notificaitonMessage = "the field " + field.Name + "is low on water";

        if (threshold.MainSensor == Utilities.Types.MainSensor.SensorUp)
        {
            // if humidity is going up return Ok()
            if (device.DeviceData.First().Humidity1 > device.DeviceData.Last().Humidity1)
            {
                return Ok();
            }
            // if humidity1 is near the low threshold on a 10% margin send notification
            if (device.DeviceData.First().Humidity1 <= threshold.Humidity1Min * 1.1)
            {
                await _fieldService.NotifyFarmAdmins(field, notificaitonMessage);
            }
        }
        else
        {
            // if humidity is going up return Ok()
            if (device.DeviceData.First().Humidity2 > device.DeviceData.Last().Humidity2)
            {
                return Ok();
            }
            // if humidity2 is near the low threshold on a 10% margin send notification
            if (device.DeviceData.First().Humidity2 <= threshold.Humidity2Min * 1.1)
            {
                await _fieldService.NotifyFarmAdmins(field, notificaitonMessage);
            }
        }


        await _alertService.CreateAlert(new()
        {
            AlertType = Utilities.Types.AlertTypes.LowThreshold,
            Field = field,
        });
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