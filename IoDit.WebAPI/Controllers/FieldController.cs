using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoDit.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class FieldController : ControllerBase
{
    private readonly IFieldService _fieldService;

    public FieldController(IFieldService fieldService)
    {
        _fieldService = fieldService;
    }

    [HttpGet("getFieldsWithDevicesForFarm/{farmId}")]
    public async Task<IActionResult> GetFieldsWithDevicesForFarm(int farmId)
    {
        var fields = await _fieldService.GetFieldsWithDevicesForFarm(new DTO.Farm.FarmDTO { Id = farmId });
        return Ok(fields);
    }

}