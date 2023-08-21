using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Repositories;

namespace IoDit.WebAPI.Services;

public class AlertService : IAlertService
{
    private readonly IAlertRepository _alertRepository;

    public AlertService(IAlertRepository alertRepository)
    {
        _alertRepository = alertRepository;
    }

    public async Task<AlertBo> CreateAlert(AlertBo alert)
    {
        Alert newAlert = await _alertRepository.CreateAlert(alert);
        alert.Id = newAlert.Id;
        return alert;
    }

    public Task<List<AlertBo>> GetAlertsByField(FieldBo field)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasActiveAlert(FieldBo field)
    {
        // TODO : check the exact rule
        await CloseOutDatedAlerts(field);
        List<Alert> alerts = await _alertRepository.GetActiveAlertsByField(field);
        return alerts.Count != 0;
    }

    public async Task CloseOutDatedAlerts(FieldBo fieldbo)
    {
        await _alertRepository.CloseOutDatedAlerts(fieldbo, 72);
    }

    public async Task DeleteAlertsFromFieldId(long fieldId)
    {
        await _alertRepository.DeleteAlertsFromFieldId(fieldId);
    }
}