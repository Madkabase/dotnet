using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public interface IFieldService
{
    public Task<List<FieldDto>> GetFieldsForFarm(FarmDTO farm);

    public Task<List<FieldDto>> GetFieldsWithDevicesForFarm(FarmDTO farm);
}