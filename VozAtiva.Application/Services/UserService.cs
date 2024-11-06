using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DocumentValidator;
using PhoneNumbers;
using VozAtiva.Domain.Entities;
using VozAtiva.Application.DTOs;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Application.Services.Interfaces;
using System;

namespace VozAtiva.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
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

        var user = _mapper.Map<User>(dto);
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return dto;
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map <IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetByEmail(string email)
    {
        var user = await _unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Email == email);

        return _mapper.Map <UserDTO> (user);
    }

    public async Task<UserDTO> GetByFederalCodeClient(string FederalCodeClient)
    {
        var user = await _unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.FederalCodeClient == FederalCodeClient);

        return _mapper.Map <UserDTO> (user);
    }

    public async Task<UserDTO> GetById(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Id == id);

        return _mapper.Map <UserDTO> (user);
    }

    public async Task<UserDTO> GetByPhone(string Phone)
    {
        var user = await _unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Phone == Phone);

        return _mapper.Map <UserDTO> (user);
    }

    public async Task Update(UserDTO dto)
    {
        var user = _mapper.Map<User>(dto);
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();
    }

    public async Task Delete(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByPropertyAsync(usr => usr.Id == id);
        await _unitOfWork.UserRepository.DeleteAsync(user);
        await _unitOfWork.CommitAsync();
    }
}