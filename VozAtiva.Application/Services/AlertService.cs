using VozAtiva.Application.Services.Interfaces;
using AutoMapper;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Application.DTOs;

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
}
