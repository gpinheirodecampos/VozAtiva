using System.ComponentModel.DataAnnotations.Schema;

namespace VozAtiva.Domain.Entities.Types;

public class BaseEntity
{
    public Guid Id { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; } = true;
    public bool Disabled { get; set; } = false;
}
