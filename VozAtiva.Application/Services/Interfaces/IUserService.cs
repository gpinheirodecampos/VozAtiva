using VozAtiva.Application.DTOs;

namespace VozAtiva.Application.Services.Interfaces;

public interface IUserService: IService <Guid, UserDTO> {
    Task<UserDTO> GetByEmail(string email);
    Task<UserDTO> GetByFederalCodeClient(string FederalCodeClient);
    Task<UserDTO> GetByPhone(string Phone);
}
