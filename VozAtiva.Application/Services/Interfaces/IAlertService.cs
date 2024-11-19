using VozAtiva.Application.DTOs;

namespace VozAtiva.Application.Services.Interfaces;

public interface IAlertService: IService <Guid, AlertDTO> {
  Task<AlertDTO> GetByTitle(string Title);
  Task<AlertDTO> GetByDate(DateTime Date);
  Task<AlertDTO> GetByPublicAgentId(int PublicAgentId);
  Task<AlertDTO> GetByAlertTypeId(int AlertTypeId);
}
