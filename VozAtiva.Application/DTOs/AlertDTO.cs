using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Enums;

namespace VozAtiva.Application.DTOs;

public record AlertDTO(
    Guid Id,
    string Title,
    string Description,
    DateTime Date,
    Guid UserId,
    int PublicAgentId,
    int AlertTypeId,
    string Status,
    string Latitude,
    string Longitude,
    UserDTO? User,
    PublicAgentDTO? PublicAgent
);
