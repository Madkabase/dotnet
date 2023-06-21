using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FieldService
{

    FieldRepository _fileRepository;

    public FieldService(FieldRepository fieldRepository)
    {
        _fileRepository = fieldRepository;
    }

    public async Task<List<FieldDto>> GetFieldsForFarm(FarmDTO farm)
    {
        var fields = await _fileRepository.GetFieldsByFarm(Farm.FromDto(farm));
        if (fields == null)
        {
            return new List<FieldDto>();
        }
        return fields.Select(f => FieldDto.FromEntity(f)).ToList();
    }

    public async Task<List<FieldDto>> GetFieldsWithDevicesForFarm(FarmDTO farm)
    {
        var farmEntity = new Farm { Id = farm.Id };
        var fields = await _fileRepository.GetFieldsWithDevicesByFarm(farmEntity);
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
            Farm = new FarmDTO
            {
                Id = f.Farm.Id,
                Name = f.Farm.Name,
            },
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
}