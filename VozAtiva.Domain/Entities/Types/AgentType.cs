using System.ComponentModel.DataAnnotations.Schema;

namespace VozAtiva.Domain.Entities.Types;

[Table("agent_type")]
public class AgentType : BaseType
{
    public ICollection<PublicAgent> PublicAgents { get; set; } = [];
}
