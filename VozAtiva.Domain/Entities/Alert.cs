using System.ComponentModel.DataAnnotations.Schema;
using VozAtiva.Domain.Entities.Types;

namespace VozAtiva.Domain.Entities;

[Table("alert")]
public class Alert : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
    public required Guid UserId { get; set; }
    public required int PublicAgentId { get; set; }
    public int AlertTypeId { get; set; }
    

    // navigation properties
    public User User { get; set; } = null!;
    public PublicAgent PublicAgent { get; set; } = null!;
    public List<Image> Images { get; set; } = [];
    public AlertType AlertType { get; set; } = null!;
}
