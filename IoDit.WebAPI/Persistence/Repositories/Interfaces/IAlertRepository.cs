using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;


public interface IAlertRepository
{
    /// <summary>
    /// Get all active alerts for a field
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    Task<List<Alert>> GetActiveAlertsByField(FieldBo field);
    /// <summary>
    /// Create a new alert
    /// </summary>
    /// <param name="alert"></param>
    /// <returns>returns the new alert</returns>
    Task<Alert> CreateAlert(AlertBo alert);
    /// <summary>
    /// Close all alerts that are older than 72 hours for a field
    /// </summary>
    /// <param name="fieldbo"></param>
    /// <returns></returns>
    Task CloseOutDatedAlerts(FieldBo fieldbo);
}