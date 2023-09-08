using IoDit.WebAPI.BO;
using IoDit.WebAPI.DTO.Field;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.DTO.Alert;

public class AlertDto
{
    public long Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public AlertTypes AlertType { get; set; } = AlertTypes.LowThreshold;
    public FieldDto Field { get; set; } = new FieldDto();
    public bool Closed { get; set; } = false;

    public static AlertDto FromBo(AlertBo alert)
    {
        return new AlertDto
        {
            Id = alert.Id,
            Date = alert.Date,
            AlertType = alert.AlertType,
            Field = FieldDto.FromBo(alert.Field),
            Closed = alert.Closed
        };
    }
}