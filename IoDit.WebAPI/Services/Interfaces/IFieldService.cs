using IoDit.WebAPI.DTO.Device;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.DTO.Threshold;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public interface IFieldService
{
    public Task<List<FieldDto>> GetFieldsForFarm(FarmDTO farm);
    public Task<List<FieldDto>> GetFieldsWithDevicesForFarm(FarmDTO farm);
    public Task<Field> CreateFieldForFarm(FieldDto field, FarmDTO farm);
    public Task<Field?> GetFieldById(long id);
    /// <summary>
    /// checks if a given user has access to a given field
    /// </summary>
    /// <param name="fieldId">the id of the field</param>
    /// <param name="user">the user to check</param>
    /// <returns>true if the user has access to the field, false otherwise</returns>
    public Task<bool> UserHasAccessToField(long fieldId, User user);
    /// <summary>
    /// checks if a given user can change a given field
    /// </summary>
    /// <param name="fieldId">the id of the field</param>
    /// <param name="user">the user to check</param>
    /// <returns>true if the user can change the field, false otherwise</returns>
    public Task<bool> UserCanChangeField(long fieldId, User user);


    public int CalculateOverAllMoistureLevel(List<DeviceDto> devices, ThresholdDto threshold);

}