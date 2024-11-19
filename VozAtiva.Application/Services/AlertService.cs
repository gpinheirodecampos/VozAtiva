using VozAtiva.Application.Services.Interfaces;
using AutoMapper;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Entities;

namespace VozAtiva.Application.Services;

public class AlertService : IAlertService 
{
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public AlertService(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<IEnumerable<AlertDTO>> GetAll()
  {
    var alerts = await _unitOfWork.AlertRepository.GetAllAsync();
    return _mapper.Map <IEnumerable<AlertDTO>>(alerts);
  }

  public async Task<AlertDTO> GetById(Guid id)
  {
    var alert = await _unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Id == id);
    
    return _mapper.Map<AlertDTO>(alert);
  }

  public async Task<AlertDTO> Add(AlertDTO dto)
  {
    if (GetByPublicAgentId(dto.PublicAgentId) == null)
    {
      throw new Exception("Id do agente publico não encontrado.");
    }

    if (GetByAlertTypeId(dto.AlertTypeId) == null)
    {
      throw new Exception("Id do tipo de alerta não encontrado.");
    }

    var alert = _mapper.Map<Alert>(dto);
    await _unitOfWork.AlertRepository.AddAsync(alert);
    await _unitOfWork.CommitAsync();

    return dto;
  }

  public async Task Update(AlertDTO dto)
  {
    var alert = _mapper.Map<Alert>(dto);
    await _unitOfWork.AlertRepository.UpdateAsync(alert);
    await _unitOfWork.CommitAsync();
  }

  public async Task Delete(Guid id)
  {
    var alert = await _unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Id == id);
    await _unitOfWork.AlertRepository.DeleteAsync(alert);
    await _unitOfWork.CommitAsync();
  }

  public async Task<AlertDTO> GetByTitle(string Title)
  {
    var alert = await _unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Title == Title);

    return _mapper.Map<AlertDTO>(alert);
  }

  public async Task<IEnumerable<AlertDTO>> GetByDate(DateTime Date)
  {
    var alerts = await _unitOfWork.AlertRepository.GetByConditionAsync(a => a.Date == Date);

    return (IEnumerable<AlertDTO>)_mapper.Map<AlertDTO>(alerts);
  }
  
  public async Task<IEnumerable<AlertDTO>> GetByPublicAgentId(int PublicAgentId)
  {
    var alerts = await _unitOfWork.AlertRepository.GetByConditionAsync(a => a.PublicAgentId == PublicAgentId);

    return (IEnumerable<AlertDTO>)_mapper.Map<AlertDTO>(alerts);
  }
  
  public async Task<IEnumerable<AlertDTO>> GetByAlertTypeId(int AlertTypeId)
  {
    var alerts = await _unitOfWork.AlertRepository.GetByConditionAsync(a => a.AlertTypeId == AlertTypeId);

    return (IEnumerable<AlertDTO>)_mapper.Map<AlertDTO>(alerts);
  }
}
