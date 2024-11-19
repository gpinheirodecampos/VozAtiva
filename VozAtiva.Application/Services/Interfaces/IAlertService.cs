using VozAtiva.Application.DTOs;

namespace VozAtiva.Application.Services.Interfaces;

public interface IAlertService: IService <Guid, AlertDTO> {
  Task<AlertDTO> GetByTitle(string Title);
  Task<IEnumerable<AlertDTO>> GetByDate(DateTime Date);
  Task<IEnumerable<AlertDTO>> GetByPublicAgentId(int PublicAgentId);
  Task<IEnumerable<AlertDTO>> GetByAlertTypeId(int AlertTypeId);
}
