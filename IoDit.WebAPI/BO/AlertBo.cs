using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.BO;

public class AlertBo
{
    public long Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public AlertTypes AlertType { get; set; } = AlertTypes.LowThreshold;
    public FieldBo Field { get; set; } = new FieldBo();
    public bool Closed { get; set; } = false;

    public static AlertBo FromEntity(Alert alert)
    {
        return new AlertBo
        {
            Id = alert.Id,
            Date = alert.Date,
            AlertType = alert.AlertType,
            Field = FieldBo.FromEntity(alert.Field),
            Closed = alert.Closed
        };
    }

}