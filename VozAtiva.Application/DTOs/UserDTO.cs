using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Enums;

namespace VozAtiva.Application.DTOs;

public class UserDTO
{
    public required string Name { get; set; }

    public required string Email { get; set; }
    public required string FederalCodeClient { get; set; }
    public required DateTime Birthdate { get; set; }
    public required string Phone { get; set; }
    public required UserTypeEnum UserType { get; set; }
}
