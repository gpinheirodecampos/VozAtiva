using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Entities.Types;

namespace VozAtiva.Application.Services.Interfaces;
public interface IPublicAgentService : IService<int, PublicAgentDTO>
{
    public Task<PublicAgentDTO> GetByName(string name);
    public Task<PublicAgentDTO> GetByAgentType(string type);
}
