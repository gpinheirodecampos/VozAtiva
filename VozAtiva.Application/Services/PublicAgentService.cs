using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using PhoneNumbers;
using RentAPI.Validations;

namespace VozAtiva.Application.Services
{
    public class PublicAgentService : IPublicAgentService
    {

        readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public PublicAgentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PublicAgentDTO>> GetAll()
        {
            var publilAgentList = await _unitOfWork.PublicAgentRepository.GetAllAsync();
            return _mapper.Map<List<PublicAgentDTO>>(publilAgentList);
        }
        public async Task<PublicAgentDTO> GetById(int id)
        {
            var publicAgent = await _unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == id);
            return _mapper.Map<PublicAgentDTO>(publicAgent);
        }
        public async Task<PublicAgentDTO> Add(PublicAgentDTO dto)
        {
            var publicAgentExists = await _unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == dto.Id
                                                                                                     || agent.Name == dto.Name
                                                                                                     || agent.Email == dto.Email
                                                                                                     || agent.Acronym == dto.Acronym
                                                                                                     || agent.Phone == dto.Phone);      

            if (publicAgentExists != null)
            {
                throw new Exception("public agent already registered");
            }

            var phoneNumberValidator = PhoneNumberUtil.GetInstance();
            var emailValidator = new EmailValidator();
            var number = phoneNumberValidator.Parse(dto.Phone, "BR");

            if (!phoneNumberValidator.IsValidNumber(number) )
            {
                throw new Exception("phone number must be in a valid format");
            }

            if (!emailValidator.IsValid(dto.Email))
            {
                throw new Exception("email must be in a valid format");
            }

            var publicAgent = _mapper.Map<PublicAgent>(dto);
            bool sucess = await _unitOfWork.PublicAgentRepository.AddAsync(publicAgent);
            await _unitOfWork.CommitAsync();

            if (sucess) return dto;
            throw new Exception("failed to register public agent");

        }
        public async Task Update(PublicAgentDTO dto)
        {
            var phoneNumberValidator = PhoneNumberUtil.GetInstance();
            var emailValidator = new EmailValidator();
            var number = phoneNumberValidator.Parse(dto.Phone, "BR");

            if (!phoneNumberValidator.IsValidNumber(number))
            {
                throw new Exception("phone number must be in a valid format");
            }

            if (!emailValidator.IsValid(dto.Email))
            {
                throw new Exception("email must be in a valid format");
            }

            var publicAgentExists = await _unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == dto.Id);

            if (publicAgentExists == null) throw new Exception("failed to update public agent. unmatched ID");

            var publicAgent = _mapper.Map<PublicAgent>(dto);
            await _unitOfWork.PublicAgentRepository.UpdateAsync(publicAgent);
            await _unitOfWork.CommitAsync();
        }
        public async Task Delete(Guid id)
        {
            //TODO request change to IService interface. method signature is bad

            /*var publicAgent = await _unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == id);

            //TODO verify if email, acronym and phone is in correct format
            await _unitOfWork.PublicAgentRepository.UpdateAsync(publicAgent);

            await _unitOfWork.CommitAsync();
            */
        }

        public async Task<PublicAgentDTO> GetByName(string name)
        {
            var publicAgent = await _unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Name == name);
            return _mapper.Map<PublicAgentDTO>(publicAgent);
        }
        public async Task<PublicAgentDTO> GetByEmail(string email)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValid(email))
            {
                throw new Exception("email must be in a valid format");
            }

            var publicAgent = await _unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Email == email);
            return _mapper.Map<PublicAgentDTO>(publicAgent);
            
        }
    }
}