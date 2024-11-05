using VozAtiva.Application.DTOs;

namespace VozAtiva.Application.Services.Interfaces;

public interface IUserService {
    Task<IEnumerable<UserDTO>> Get();
    Task<UserDTO> GetById(Guid id);
    Task<UserDTO> GetByEmail(string email);
    Task<UserDTO> GetByFederalCodeClient(string FederalCodeClient);
    Task<UserDTO> GetByPhone(string Phone);
    Task<UserDTO> Add(UserDTO userDto);
    Task Update(UserDTO userDto);
    Task Delete(Guid id);
}
