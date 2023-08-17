using IoDit.WebAPI.BO;

namespace IoDit.WebAPI.Services;

public interface IAlertService
{
    /// <summary>
    /// Checks if a field has an active alert
    /// </summary>
    /// <param name="field">the field to check</param>
    /// <returns>a boolean</returns>
    Task<bool> hasActiveAlert(FieldBo field);
    /// <summary>
    /// Gets all active alerts for a field
    /// </summary>
    /// <param name="field"></param>
    /// <returns>the list of the alerts</returns>
    Task<List<AlertBo>> GetAlertsByField(FieldBo field);
    /// <summary>
    /// Creates an alert
    /// </summary>
    /// <param name="alert"></param>
    /// <returns>the created alert</returns>
    Task<AlertBo> CreateAlert(AlertBo alert);

}