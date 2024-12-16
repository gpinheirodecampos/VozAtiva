using VozAtiva.Application.DTOs;

namespace VozAtiva.Application.Services.Interfaces;

public interface IAlertService: IService <Guid, AlertDTO> {
  Task<AlertDTO> GetByTitle(string Title);
  Task<IEnumerable<AlertDTO>> GetByDate(DateTime Date);
  Task<IEnumerable<AlertDTO>> GetByPublicAgentId(int PublicAgentId);
  Task<IEnumerable<AlertDTO>> GetByAlertTypeId(int AlertTypeId);
  Task<IEnumerable<AlertDTO>> GetByCoordinateRangeAroundPoint(double latitude, double longitude, double latRange, double longRange);
  Task<IEnumerable<AlertDTO>> GetByCoordinateRange(double latMin, double latMax, double longMin, double longMax);
}
