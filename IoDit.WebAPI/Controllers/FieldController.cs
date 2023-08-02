using System.Security.Claims;
using IoDit.WebAPI.DTO;
using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
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

    public FieldController(
        IFieldService fieldService,
        IUserService userService,
        IFarmUserService farmUserService,
        IThresholdService thresholdService
    )
    {
        _fieldService = fieldService;
        _userService = userService;
        _farmUserService = farmUserService;
        _thresholdService = thresholdService;
    }

    [HttpGet("getFieldsWithDevicesForFarm/{farmId}")]
    public async Task<IActionResult> GetFieldsWithDevicesForFarm(int farmId)
    {
        var fields = await _fieldService.GetFieldsWithDevicesForFarm(new DTO.Farm.FarmDTO { Id = farmId });
        fields = fields.Select(f =>
         {
             f.OverallMoistureLevel = _fieldService.CalculateOverAllMoistureLevel(f.Devices, f.Threshold);
             return f;
         }).ToList();
        return Ok(fields);
    }



    [HttpPost("createField")]
    public async Task<IActionResult> CreateField([FromBody] CreateFieldRequestDTO createFieldDTO)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }

        var userFarm = await _farmUserService.GetUserFarm(createFieldDTO.Farm.Id, user.Id);
        if (userFarm == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this farm" });
        }
        if (userFarm.FarmRole != FarmRoles.Admin && AppRoles.AppAdmin != user.AppRole)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this farm" });
        }

        var field = new FieldDto
        {
            Name = createFieldDTO.FieldName,
            Geofence = NetTopologySuite.Geometries.Geometry.DefaultFactory.CreatePolygon(
                createFieldDTO.Coordinates.Select(c => new NetTopologySuite.Geometries.Coordinate(c[0], c[1])).ToArray()
            ),
            Threshold = createFieldDTO.Threshold,
        };

        var farm = new DTO.Farm.FarmDTO { Id = createFieldDTO.Farm.Id };


        var fieldE = (await _fieldService.CreateFieldForFarm(field, farm));
        return Ok(FieldDto.FromEntity(fieldE));
    }

    [HttpGet("getFieldDetails/{fieldId}")]
    public async Task<IActionResult> GetFieldDetails(int fieldId)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "User not found" });
        }

        if (!await _fieldService.UserHasAccessToField(fieldId, user))
        {
            return BadRequest(new ErrorResponseDTO { Message = "User does not have access to this field" });
        }
        var field = await _fieldService.GetFieldById(fieldId);
        if (field == null)
        {
            return BadRequest(new ErrorResponseDTO { Message = "Field not found" });
        }
        var fieldDto = new FieldDto
        {
            Id = field.Id,
            Name = field.Name,
            Geofence = field.Geofence,
            Threshold = field.Threshold != null ? ThresholdDto.FromEntity(field.Threshold) : null,
            Devices = field.Devices.Select(d => DeviceDto.FromEntity(d)).ToList()
        };
        return Ok(fieldDto);
    }

    [HttpPatch("{fieldId}/updateThreshold")]
    public async Task<IActionResult> UpdateThreshold(int fieldId, [FromBody] ThresholdDto thresholdDto)
    {
        var user = await GetRequestDetails();
        if (user == null)
        {
            return NotFound(new ErrorResponseDTO { Message = "User not found" });
        }
        var field = await _fieldService.GetFieldById(fieldId);
        if (field == null)
        {
            return NotFound(new ErrorResponseDTO { Message = "Field not found" });
        }
        if (!await _fieldService.UserCanChangeField(fieldId, user))
        {
            return Unauthorized(new ErrorResponseDTO { Message = "User does no right to modify field" });
        }
        var threshold = await _thresholdService.UpdateThreshold(thresholdDto);
        if (threshold == null)
        {
            return NotFound(new ErrorResponseDTO { Message = "Threshold not found" });
        }
        return Ok(ThresholdDto.FromEntity(threshold));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<User?> GetRequestDetails()
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;
        var userIdClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        var userId = userIdClaim?.Value;
        if (userId == null)
        {
            return null;
        }
        var user = await _userService.GetUserByEmail(userId);
        if (user != null)
        {
            return user;
        }

        return null;
    }
}