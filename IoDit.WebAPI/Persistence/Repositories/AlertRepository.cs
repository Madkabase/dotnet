using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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

    public Task CloseOutDatedAlerts(FieldBo fieldbo, int hours)
    {
        string sql = "UPDATE \"Alerts\" SET  \"Closed\" = true "
        + "WHERE \"Id\" in (select a.\"Id\" from \"Alerts\" a where a.\"FieldId\" = {0} and a.\"Date\" < '{1}' )"
        + ";";
        return _context.Database.ExecuteSqlRawAsync(string.Format(sql, fieldbo.Id, DateTime.Now.ToLocalTime().AddHours(-hours).ToString("yyyy-MM-dd HH:mm:ss.fff zzz").Remove(27, 1)));
    }

    public Task DeleteAlertsFromFieldId(long fieldId)
    {
        _context.RemoveRange(_context.Alerts.Where(a => a.FieldId == fieldId));
        return _context.SaveChangesAsync();
    }
}
