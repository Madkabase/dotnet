using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;

namespace IoDit.WebAPI.Persistence.Repositories;


public class AlertRepository : IAlertRepository
{

    private readonly AgroditDbContext _context;

    public AlertRepository(AgroditDbContext context)
    {
        _context = context;
    }

    public Task<Alert> CreateAlert(AlertBo alert)
    {
        Alert newAlert = new()
        {
            AlertType = alert.AlertType,
            Closed = false,
            FieldId = alert.Field.Id,
            Date = DateTime.Now.ToUniversalTime(),
        };
        var res = _context.Alerts.Add(newAlert);
        _context.SaveChanges();
        return Task.FromResult(res.Entity);

        throw new NotImplementedException();
    }

    public Task<List<Alert>> GetActiveAlertsByField(FieldBo field)
    {
        return Task.Run(() => _context.Alerts.Where(a => a.FieldId == field.Id)
          .Where(a => !a.Closed).ToList()
        );
    }

    public Task CloseOutDatedAlerts(FieldBo fieldbo)
    {
        return Task.Run(() =>
        {
            var alerts = _context.Alerts.Where(a => a.FieldId == fieldbo.Id)
            .Where(a => !a.Closed)
            .Where(a => a.Date < DateTime.Now.ToUniversalTime().AddHours(-72)).ToList();
            foreach (var alert in alerts)
            {

                alert.Closed = true;
            }
            _context.Alerts.UpdateRange(alerts);
            _context.SaveChanges();
        });
    }


}
