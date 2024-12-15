using VozAtiva.Application.Services.Interfaces;
using AutoMapper;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Entities;

namespace VozAtiva.Application.Services;

public class AlertService(IUnitOfWork unitOfWork, IMapper mapper, ISendEmailService emailService) : IAlertService 
{
    public async Task<IEnumerable<AlertDTO>> GetAll()
    {
        var alerts = await unitOfWork.AlertRepository.GetAllAsync();
        return mapper.Map <IEnumerable<AlertDTO>>(alerts);
    }

    public async Task<AlertDTO> GetById(Guid id)
    {
        var alert = await unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Id == id);
    
        return mapper.Map<AlertDTO>(alert);
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

        var alert = mapper.Map<Alert>(dto);

        await unitOfWork.AlertRepository.AddAsync(alert);

        await unitOfWork.CommitAsync();

        var user = await unitOfWork.UserRepository.GetByPropertyAsync(u => u.Id == alert.UserId);

        await emailService.EnqueueSendEmailAsync(alert.Id, user.Name, user.Email);

        return dto;
    }

    public async Task Update(AlertDTO dto)
    {
        var alert = mapper.Map<Alert>(dto);
        await unitOfWork.AlertRepository.UpdateAsync(alert);
        await unitOfWork.CommitAsync();
    }

    public async Task Delete(Guid id)
    {
        var alert = await unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Id == id);
        await unitOfWork.AlertRepository.DeleteAsync(alert);
        await unitOfWork.CommitAsync();
    }

    public async Task<AlertDTO> GetByTitle(string Title)
    {
        var alert = await unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Title == Title);

        return mapper.Map<AlertDTO>(alert);
    }

    public async Task<IEnumerable<AlertDTO>> GetByDate(DateTime Date)
    {
        var alerts = await unitOfWork.AlertRepository.GetByConditionAsync(a => a.Date == Date);

        return (IEnumerable<AlertDTO>)mapper.Map<AlertDTO>(alerts);
    }
  
    public async Task<IEnumerable<AlertDTO>> GetByPublicAgentId(int PublicAgentId)
    {
        var alerts = await unitOfWork.AlertRepository.GetByConditionAsync(a => a.PublicAgentId == PublicAgentId);

        return (IEnumerable<AlertDTO>)mapper.Map<AlertDTO>(alerts);
    }
  
    public async Task<IEnumerable<AlertDTO>> GetByAlertTypeId(int AlertTypeId)
    {
        var alerts = await unitOfWork.AlertRepository.GetByConditionAsync(a => a.AlertTypeId == AlertTypeId);

        return (IEnumerable<AlertDTO>)mapper.Map<AlertDTO>(alerts);
    }

    public async Task Delete(AlertDTO dto)
    {
        var alert = await unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Id == dto.Id) ?? throw new Exception("Alerta não encontrado.");

        await unitOfWork.AlertRepository.DeleteAsync(alert);
    }
}
