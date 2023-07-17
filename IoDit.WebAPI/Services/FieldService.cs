using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class FieldService : IFieldService
{

    IFieldRepository _fieldRepository;
    IFarmUserService _farmUserService;

    public FieldService(IFieldRepository fieldRepository,
        IFarmUserService farmUserService)
    {
        _fieldRepository = fieldRepository;
        _farmUserService = farmUserService;
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
                Id = d.DevEUI,
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
            }).ToList(),
            Threshold = f.Threshold != null ? new ThresholdDto
            {
                Id = f.Threshold.Id,
                Humidity1Min = f.Threshold.Humidity1Min,
                Humidity1Max = f.Threshold.Humidity1Max,
                Humidity2Min = f.Threshold.Humidity2Min,
                Humidity2Max = f.Threshold.Humidity2Max,
                TemperatureMin = f.Threshold.TemperatureMin,
                TemperatureMax = f.Threshold.TemperatureMax,
                BatteryLevelMin = f.Threshold.BatteryLevelMin,
                BatteryLevelMax = f.Threshold.BatteryLevelMax

            } : null
        })
        .ToList();
    }

    public async Task<FieldDto> CreateFieldForFarm(FieldDto field, FarmDTO farm)
    {

        var fieldEntity = new Field { Name = field.Name, Farm = new Farm { Id = farm.Id }, Geofence = field.Geofence };

        return FieldDto.FromEntity(await _fieldRepository.CreateField(fieldEntity));
    }

    public async Task<Field?> GetFieldById(long id)
    {
        var field = await _fieldRepository.GetFieldById(id);

        if (field == null)
        {
            return null;
        }
        return field;
    }

    public async Task<bool> UserHasAccessToField(long fieldId, User user)
    {
        // throw new NotImplementedException();



        var field = await _fieldRepository.GetFieldById(fieldId);
        if (field == null)
        {
            return false;
        }

        // TODO : when FieldUser is created, implement this

        var d = await _farmUserService.HasAccessToFarm(field.Farm, user);

        return d;
        // return await _fieldRepository.UserHasAccessToField(fieldId, user);
    }
}