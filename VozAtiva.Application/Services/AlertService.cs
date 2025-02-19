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
        return mapper.Map<IEnumerable<AlertDTO>>(alerts);
    }

    public async Task<AlertDTO> GetById(Guid id)
    {
        var alert = await unitOfWork.AlertRepository.GetByPropertyAsync(a => a.Id == id);

        return mapper.Map<AlertDTO>(alert);
    }

    public async Task<AlertDTO> Add(AlertDTO dto)
    {
        var alert = mapper.Map<Alert>(dto);

        await unitOfWork.AlertRepository.AddAsync(alert);

        await unitOfWork.CommitAsync();

        var user = await unitOfWork.UserRepository.GetByPropertyAsync(u => u.Id == alert.UserId);

        await emailService.EnqueueSendEmailAsync(alert.Id, user.Name, user.Email, alert.Title);
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
    public async Task<IEnumerable<AlertDTO>> GetByCoordinateRangeAroundPoint(double latitude, double longitude, double latRange, double longRange)
    {
        var alerts = await unitOfWork.AlertRepository
            .GetByConditionAsync(alert => (alert.Latitude < alert.Latitude + latRange)
                                        && (alert.Latitude > alert.Latitude - latRange)
                                        && (alert.Longitude < alert.Longitude + longRange)
                                        && (alert.Longitude > alert.Longitude - longRange));

        return mapper.Map<IEnumerable<AlertDTO>>(alerts);
    }

    public async Task<IEnumerable<AlertDTO>> GetByCoordinateRange(double latMin, double latMax, double longMin, double longMax)
    {
        var alerts = await unitOfWork.AlertRepository.GetByPropertyAsync(a => (a.Latitude < latMax && a.Latitude > latMin)
                                                                            && (a.Longitude < longMax && a.Longitude > longMin));
        return mapper.Map<IEnumerable<AlertDTO>>(alerts);
    }

    public async Task<IEnumerable<AlertDTO>> GetByDistance(double lat, double lon, double distance)
    {
        if (lat < -90 || lat > 90)
        {
            throw new Exception("Latitude fora do intervalo permitido");
        }
        if (lon < -180 || lon > 180)
        {
            throw new Exception("Longitude fora do intervalo");
        }
        double distInLatitude = distance / 222;
        double KmPerLongitudeUnit = 111 - (111*(Math.Abs(lat)/90));
        double distInLongitude = distance / KmPerLongitudeUnit;
        IEnumerable<Alert> alerts = await unitOfWork.AlertRepository.GetAllAsync();
        IEnumerable<Alert> filteredAlerts = alerts.Where(alert => (alert.Latitude < lat + distInLatitude) && (alert.Latitude > lat - distInLatitude)
                                                                    && (alert.Longitude < lon + distInLongitude) && (alert.Longitude > lon - distInLongitude)).ToList();
        /*IEnumerable<Alert> filteredAlerts = alerts.Where(a => (Math.Pow(a.Latitude - lat,2) + Math.Pow(a.Longitude - lon,2)) <= Math.Pow(distance,2));*/
        return mapper.Map<IEnumerable<AlertDTO>>(filteredAlerts);
    }

}
