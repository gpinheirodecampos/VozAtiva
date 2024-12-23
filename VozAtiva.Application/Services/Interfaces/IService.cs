using VozAtiva.Application.DTOs;

namespace VozAtiva.Application.Services.Interfaces;

public interface IService <TId, Tdto> {
    Task<IEnumerable<Tdto>> GetAll();
    Task<Tdto> GetById(TId id);
    Task<Tdto> Add(Tdto dto);
    Task Update(Tdto dto);
    Task Delete(Tdto dto);
}
