using System.ComponentModel.DataAnnotations.Schema;
using VozAtiva.Domain.Entities.Types;
using VozAtiva.Domain.Enums;

namespace VozAtiva.Domain.Entities;

[Table("alert")]
public class Alert : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime Date
    {
        get => _date;
        set => _date = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
    }
    private DateTime _date;
    public required Guid UserId { get; set; }
    public required int PublicAgentId { get; set; }
    public int AlertTypeId { get; set; }
    public AlertStatusEnum Status { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // navigation properties
    public User User { get; set; } = null!;
    public PublicAgent PublicAgent { get; set; } = null!;
    public List<Image> Images { get; set; } = [];
    public AlertType AlertType { get; set; } = null!;
}
