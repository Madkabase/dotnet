using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Farm;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Services;

public interface IFieldService
{
    public Task<List<FieldBo>> GetFieldsForFarm(FarmBo farm);
    public Task<List<FieldBo>> GetFieldsWithDevicesForFarm(FarmBo farm);
    public Task<FieldBo> CreateFieldForFarm(FieldBo field, FarmBo farm);
    public Task<FieldBo> GetFieldById(long id);
    /// <summary>
    /// checks if a given user has access to a given field
    /// </summary>
    /// <param name="fieldId">the id of the field</param>
    /// <param name="user">the user to check</param>
    /// <returns>true if the user has access to the field, false otherwise</returns>
    public Task<bool> UserHasAccessToField(long fieldId, UserBo user);
    /// <summary>
    /// checks if a given user can change a given field
    /// </summary>
    /// <param name="fieldId">the id of the field</param>
    /// <param name="user">the user to check</param>
    /// <returns>true if the user can change the field, false otherwise</returns>
    public Task<bool> UserCanChangeField(long fieldId, UserBo user);


    public int CalculateOverAllMoistureLevel(List<DeviceBo> devices, ThresholdBo threshold);

}