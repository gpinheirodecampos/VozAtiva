using AutoMapper;
using DocumentValidator;
using PhoneNumbers;
using VozAtiva.Domain.Entities;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Application.Services.Interfaces;

namespace VozAtiva.Application.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async Task<UserDTO> Add(UserDTO dto)
    {
        if (!CpfValidation.Validate(dto.FederalCodeClient)) {
            throw new Exception("CPF inválido.");
        }
        
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        var number = phoneNumberUtil.Parse(dto.Phone, "BR");
        if (!phoneNumberUtil.IsValidNumber(number)) {
            throw new Exception("Número de telefone inválido");
        }
        if (GetByEmail(dto.Email) != null) {
            throw new Exception("E-mail já cadastrado.");
        }
        if (GetByFederalCodeClient(dto.FederalCodeClient) != null) {
            throw new Exception("CPF já cadastrado.");
        }
        if (GetByPhone(dto.Phone) != null) {
            throw new Exception("Número de telefone já cadastrado.");
        }

        var user = mapper.Map<User>(dto);
        await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.CommitAsync();

        return dto;
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await unitOfWork.UserRepository.GetAllAsync();
        return mapper.Map <IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetByEmail(string email)
    {
        var user = await unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Email == email);

        return mapper.Map <UserDTO> (user);
    }

    public async Task<UserDTO> GetByFederalCodeClient(string FederalCodeClient)
    {
        var user = await unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.FederalCodeClient == FederalCodeClient);

        return mapper.Map <UserDTO> (user);
    }

    public async Task<UserDTO> GetById(Guid id)
    {
        var user = await unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Id == id);

        return mapper.Map <UserDTO> (user);
    }

    public async Task<UserDTO> GetByPhone(string Phone)
    {
        var user = await unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Phone == Phone);

        return mapper.Map <UserDTO> (user);
    }

    public async Task Update(UserDTO dto)
    {
        var user = mapper.Map<User>(dto);
        await unitOfWork.UserRepository.UpdateAsync(user);
        await unitOfWork.CommitAsync();
    }

    public async Task Delete(UserDTO dto)
    {
        var user = mapper.Map<User>(dto);
        await unitOfWork.UserRepository.DeleteAsync(user);
        await unitOfWork.CommitAsync();
    }
}