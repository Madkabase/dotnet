using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FieldService : IFieldService
{

    IFieldRepository _fieldRepository;

    public FieldService(IFieldRepository fieldRepository)
    {
        _fieldRepository = fieldRepository;
    }

    public async Task<List<FieldDto>> GetFieldsForFarm(FarmDTO farm)
    {
        var fields = await _fieldRepository.GetFieldsByFarm(Farm.FromDto(farm));
        if (fields == null)
        {
            return new List<FieldDto>();
        }
        return fields.Select(f => FieldDto.FromEntity(f)).ToList();
    }

    public async Task<List<FieldDto>> GetFieldsWithDevicesForFarm(FarmDTO farm)
    {
        var farmEntity = new Farm { Id = farm.Id };
        var fields = await _fieldRepository.GetFieldsWithDevicesByFarm(farmEntity);
        if (fields == null)
        {
            return new List<FieldDto>();
        }
        return fields
        .Select(f => { f.Farm = farmEntity; return f; })
        .Select(f => new FieldDto
        {
            Id = f.Id,
            Name = f.Name,
            Geofence = f.Geofence,
            Devices = f.Devices.Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                Data = d.DeviceData.Select(dd => new DeviceDataDTO
                {
                    Id = dd.Id,
                    BatteryLevel = dd.BatteryLevel,
                    Humidity1 = dd.Humidity1,
                    Humidity2 = dd.Humidity2,
                    Temperature = dd.Temperature,
                    TimeStamp = dd.TimeStamp
                }).ToList()
            }).ToList()
        })
        .ToList();
    }

    public async Task<FieldDto> CreateFieldForFarm(FieldDto field, FarmDTO farm)
    {

        var fieldEntity = new Field { Name = field.Name, Farm = new Farm { Id = farm.Id }, Geofence = field.Geofence };

        return FieldDto.FromEntity(await _fieldRepository.CreateField(fieldEntity));
    }
}