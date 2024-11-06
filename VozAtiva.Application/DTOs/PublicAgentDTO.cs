using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VozAtiva.Domain.Entities.Types;
using VozAtiva.Domain.Entities;

namespace VozAtiva.Application.DTOs;
public record PublicAgentDTO(
    int Id,
    string Name,
    string? Email,
    string? Phone,
    string? Acronym,
    int AgentTypeId
);