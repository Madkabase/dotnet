using IoDit.WebAPI.BO;
using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities;

public class Alert : EntityBase, IEntity
{
    public DateTime Date { get; set; }
    public AlertTypes AlertType { get; set; }

    public long? FieldId { get; set; }
    public virtual Field Field { get; set; }
    public bool Closed { get; set; } = false;

    public static Alert FromBo(AlertBo alert)
    {
        return new Alert
        {
            Id = alert.Id,
            Date = alert.Date,
            AlertType = alert.AlertType,
            FieldId = alert.Field.Id,
            Field = Field.FromBo(alert.Field),
            Closed = alert.Closed
        };
    }
}