using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Enums;

namespace VozAtiva.Application.DTOs;

public record UserDTO(
    Guid Id,
    string Name,
    string Email,
    string FederalCodeClient,
    DateTime Birthdate,
    string Phone,
    UserTypeEnum UserType
);
