using System.ComponentModel.DataAnnotations.Schema;

namespace VozAtiva.Domain.Entities.Types;

[Table("alert_type")]
public class AlertType : BaseType
{
    public ICollection<Alert> Alerts { get; set; } = [];
}
