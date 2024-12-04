using AutoMapper;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Domain.Entities;
using VozAtiva.Domain.Interfaces;
using PhoneNumbers;
using RentAPI.Validations;
using System.Runtime.CompilerServices;

namespace VozAtiva.Application.Services
{
    public class PublicAgentService(IUnitOfWork unitOfWork, IMapper mapper) : IPublicAgentService
    {

        public IUnitOfWork GetUnitOfWork()
        {
            return unitOfWork;
        }
        public async Task<IEnumerable<PublicAgentDTO>> GetAll()
        {
            var publilAgentList = await unitOfWork.PublicAgentRepository.GetAllAsync();
            return mapper.Map<List<PublicAgentDTO>>(publilAgentList);

        }
        public async Task<PublicAgentDTO> GetById(int id)
        {
            var publicAgent = await unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == id);
            return mapper.Map<PublicAgentDTO>(publicAgent);
        }
        public async Task<PublicAgentDTO> Add(PublicAgentDTO dto)
        {
            var publicAgentExists = await unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == dto.Id
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

            if(publicAgentExists != null && publicAgentExists.Id == dto.Id)
            {
                throw new Exception("ID matched with that of another Public Agent");
            }

            if (!phoneNumberValidator.IsValidNumber(number) )
            {
                throw new Exception("phone number must be in a valid format");
            }

            if (!emailValidator.IsValid(dto.Email))
            {
                throw new Exception("email must be in a valid format");
            }

            var publicAgent = mapper.Map<PublicAgent>(dto);
            bool sucess = await unitOfWork.PublicAgentRepository.AddAsync(publicAgent);
            await unitOfWork.CommitAsync();

            if (sucess) 
            {
                await unitOfWork.CommitAsync();
                return dto;
            }
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

            var publicAgentExists = await unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Id == dto.Id) ?? throw new Exception("failed to update public agent. unmatched ID");
            //var publicAgent = mapper.Map<PublicAgent>(dto);
            publicAgentExists.Name = dto.Name;
            await unitOfWork.PublicAgentRepository.UpdateAsync(publicAgentExists);
            await unitOfWork.CommitAsync();
        }
        public async Task Delete(PublicAgentDTO dto)
        {
            var agent = mapper.Map<PublicAgent>(dto);

            await unitOfWork.PublicAgentRepository.DeleteAsync(agent);

            await unitOfWork.CommitAsync();
        }

        public async Task<PublicAgentDTO> GetByName(string name)
        {
            var publicAgent = await unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Name == name);
            return mapper.Map<PublicAgentDTO>(publicAgent);
        }
        public async Task<PublicAgentDTO> GetByEmail(string email)
        {
            var emailValidator = new EmailValidator();

            if (!emailValidator.IsValid(email))
            {
                throw new Exception("email must be in a valid format");
            }

            var publicAgent = await unitOfWork.PublicAgentRepository.GetByPropertyAsync(agent => agent.Email == email);
            return mapper.Map<PublicAgentDTO>(publicAgent);
            
        }
    }
}