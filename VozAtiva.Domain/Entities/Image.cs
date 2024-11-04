using System.ComponentModel.DataAnnotations.Schema;
using VozAtiva.Domain.Entities.Types;

namespace VozAtiva.Domain.Entities;

[Table("image")]
public class Image : BaseEntity
{
    public required string Url { get; set; }
    public string? Description { get; set; }
    public string? FileName { get; set; }
    public Guid? AlertId { get; set; }

    // navigation properties
    public Alert Alert { get; set; } = null!;
}
