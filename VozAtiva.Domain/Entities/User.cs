using System.ComponentModel.DataAnnotations.Schema;
using VozAtiva.Domain.Entities.Types;
using VozAtiva.Domain.Enums;

namespace VozAtiva.Domain.Entities;

[Table("user")]
public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string FederalCodeClient { get; set; }
    public DateTime Birthdate
    {
        get => _birthdate;
        set => _birthdate = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);
    }
    private DateTime _birthdate;
    public required string Phone { get; set; }
    public required UserTypeEnum UserType { get; set; }


    // navigation properties
    public List<Alert> Alerts { get; set; } = [];
}
