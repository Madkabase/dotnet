using System.Security.Claims;
using IoDit.WebAPI.BO;
using IoDit.WebAPI.Config.Exceptions;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.DTO.User;
using IoDit.WebAPI.Services;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FieldController : ControllerBase, IBaseController
{
    private readonly IUserService _userService;
    private readonly IFieldService _fieldService;
    private readonly IFarmUserService _farmUserService;
    private readonly IThresholdService _thresholdService;
    private readonly IFieldUserService _fieldUserService;

    public FieldController(
        IFieldService fieldService,
        IUserService userService,
        IFarmUserService farmUserService,
        IThresholdService thresholdService,
        IFieldUserService fieldUserService
    )
    {
        _fieldService = fieldService;
        _userService = userService;
        _farmUserService = farmUserService;
        _thresholdService = thresholdService;
        _fieldUserService = fieldUserService;
    }

    [HttpGet("getFieldsWithDevicesForFarm/{farmId}")]
    public async Task<ActionResult<List<FieldDto>>> GetFieldsWithDevicesForFarm(int farmId)
    {
        List<FieldBo> fields = await _fieldService.GetFieldsWithDevicesForFarm(new BO.FarmBo { Id = farmId });

        List<FieldDto> fieldDtos = fields.Select(f => FieldDto.FromBo(f)).ToList();

        foreach (var (fieldDto, i) in fieldDtos.Select((value, i) => (value, i)))
        {
            fieldDto.OverallMoistureLevel = _fieldService.CalculateOverAllMoistureLevel(fields[i].Devices.ToList(), fields[i].Threshold);
        }

        return Ok(fieldDtos);
    }



    [HttpPost("createField")]
    public async Task<ActionResult> CreateField([FromBody] CreateFieldRequestDTO createFieldDTO)
    {
        var user = await GetRequestDetails();

        var userFarm = await _farmUserService.GetUserFarm(createFieldDTO.FarmId, user.Id)
            ?? throw new UnauthorizedAccessException("User does not have access to this farm");
        if (userFarm.FarmRole != FarmRoles.Admin && AppRoles.AppAdmin != user.AppRole)
        {
            throw new UnauthorizedAccessException("User does not have access to this farm");
        }

        var field = new FieldBo
        {
            Name = createFieldDTO.FieldName,
            Geofence = NetTopologySuite.Geometries.Geometry.DefaultFactory.CreatePolygon(
                createFieldDTO.Coordinates.Select(c => new NetTopologySuite.Geometries.Coordinate(c[0], c[1])).ToArray()
            ),
            Threshold = ThresholdBo.FromDto(createFieldDTO.Threshold),
        };

        var farm = new BO.FarmBo { Id = createFieldDTO.FarmId };

        FieldBo fieldBo = await _fieldService.CreateFieldForFarm(field, farm);
        return Ok(FieldDto.FromBo(fieldBo));
    }

    [HttpGet("getFieldDetails/{fieldId}")]
    public async Task<ActionResult<FieldDto>> GetFieldDetails(int fieldId)
    {
        var user = await GetRequestDetails();

        if (!await _fieldService.UserHasAccessToField(fieldId, user))
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this field" });
        }
        var field = await _fieldService.GetFieldByIdFull(fieldId)
            ?? throw new EntityNotFoundException("Field not found");

        return Ok(FieldDto.FromBo(field));
    }

    [HttpPatch("{fieldId}/updateThreshold")]
    public async Task<ActionResult<ThresholdDto>> UpdateThreshold(int fieldId, [FromBody] ThresholdDto thresholdDto)
    {
        var user = await GetRequestDetails();

        _ = await _fieldService.GetFieldByIdFull(fieldId)
            ?? throw new EntityNotFoundException("Field not found");


        if (!await _fieldService.UserCanChangeField(fieldId, user))
        {
            throw new UnauthorizedAccessException("User has no right to modify field");
        }
        var threshold = await _thresholdService.UpdateThreshold(ThresholdBo.FromDto(thresholdDto))
            ?? throw new EntityNotFoundException("Threshold not found");

        return Ok(ThresholdDto.FromBo(threshold));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<BO.UserBo> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = (userIdClaim?.Value)
            ?? throw new UnauthorizedAccessException("Invalid user");
        var user = await _userService.GetUserByEmail(userId);
        return user;
    }

    [HttpGet("{fieldId}")]
    public async Task<ActionResult<FieldDto>> GetField(int fieldId)
    {
        var user = await GetRequestDetails();
        if (!await _fieldService.UserHasAccessToField(fieldId, user))
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this field" });
        }
        var field = await _fieldService.GetFieldByIdFull(fieldId)
            ?? throw new EntityNotFoundException("Field not found");

        var fieldDto = FieldDto.FromBo(field);
        fieldDto.OverallMoistureLevel = _fieldService.CalculateOverAllMoistureLevel(field.Devices.ToList(), field.Threshold);
        return Ok(fieldDto);
    }

    [HttpPut("{fieldId}/addFarmer")]
    public async Task<ActionResult> AddFarmer(int fieldId, [FromBody] string farmerEmail)
    {
        var user = await GetRequestDetails();

        if (!await _fieldService.UserCanChangeField(fieldId, user))
        {
            throw new UnauthorizedAccessException("User has no right to modify field");
        }

        var farmerToAdd = await _userService.GetUserByEmail(farmerEmail);

        await _fieldUserService.AddFieldUser(new FieldBo { Id = fieldId }, farmerToAdd, FieldRoles.User);

        return Ok();

    }

    [HttpGet("{fieldId}/devices")]
    public Task<ActionResult<List<DeviceDto>>> GetDevicesForField(int fieldId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{fieldId}/threshold")]
    public Task<ActionResult<List<ThresholdDto>>> GetThresholdForField(int fieldId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{fieldId}/owner")]
    public Task<ActionResult<UserDto>> GetOwnerForField(int fieldId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{fieldId}/farm")]
    public Task<ActionResult<FarmDto>> GetFarmForField(int fieldId)
    {
        throw new NotImplementedException();
    }
}