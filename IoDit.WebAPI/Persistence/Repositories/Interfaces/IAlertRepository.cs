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
    /// Close all alerts that are older than the given hours
    /// </summary>
    /// <param name="fieldbo">the field where alerts are closed</param>
    /// <param name="hours">the considered as outdated</param>
    /// <returns></returns>
    Task CloseOutDatedAlerts(FieldBo fieldbo, int hours);
}