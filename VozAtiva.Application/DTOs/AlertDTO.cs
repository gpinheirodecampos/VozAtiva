using System.Text.Json.Serialization;
using VozAtiva.Domain.Enums;

namespace VozAtiva.Application.DTOs;

public record AlertDTO(Guid Id, string Title, string Description, DateTime Date, Guid UserId, int PublicAgentId, int AlertTypeId, double Latitude, double Longitude, AlertStatusEnum Status);
