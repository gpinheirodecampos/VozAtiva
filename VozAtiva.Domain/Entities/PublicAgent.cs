using System.ComponentModel.DataAnnotations.Schema;
using VozAtiva.Domain.Entities.Types;

namespace VozAtiva.Domain.Entities;

[Table("public_agent")]
public class PublicAgent
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Acronym { get; set; }
    public required int AgentTypeId { get; set; }

    // navigation properties
    public AgentType AgentType { get; set; } = null!;
    public List<Alert> Alerts { get; set; } = [];
}
