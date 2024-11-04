namespace VozAtiva.Domain.Entities.Types;

public class BaseType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
