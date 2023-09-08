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
    public Task<FieldBo> GetFieldByIdFull(long id);
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
    /// <summary>
    /// calculates the overall moisture level of a field
    /// </summary>
    /// <param name="devices"></param>
    /// <param name="threshold"></param>
    /// <returns>the overall level moisture in pourcentage</returns>
    public int CalculateOverAllMoistureLevel(List<DeviceBo> devices, ThresholdBo threshold);
    /// <summary>
    /// gets the field where a given device EUI is
    /// </summary>
    /// <param name="devEui"></param>
    /// <returns></returns>
    Task<FieldBo> GetFieldFromDeviceEui(string devEui);
    /// <summary>
    /// sends a notification to all farm admins of a given field
    /// </summary>
    /// <param name="field"></param>
    /// <param name="notificationBody"></param>
    /// <returns></returns>
    Task NotifyFarmAdmins(FieldBo field, string notificationBody);
    /// <summary>
    /// deletes a field and all data related to : devices and related data, threshold, alert, fieldUsers, farmFields, field
    /// </summary>
    /// <param name="fieldId"></param>
    /// <returns></returns>
    Task DeleteField(long fieldId);
}