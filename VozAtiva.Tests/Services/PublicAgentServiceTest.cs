using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Domain.Interfaces;
using VozAtiva.Infrastructure.Context;
using System.Linq;
using VozAtiva.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Xml.Linq;
using VozAtiva.Domain.Entities.Types;

namespace VozAtiva.Tests.Services;
public class PublicAgentServiceTest : ServiceTestsBase<PublicAgentService>
{
    [Fact]
    public async Task Verify_Unit_Of_Work_And_Mapper()
    {
        var uow = _service.GetUnitOfWork();
        uow.Should().NotBeNull();
    }
    [Fact]
    public async Task Get_All_Public_Agents()
    {
        var pa1 = new PublicAgent
        {
            Id = 11,
            Name = "Camara Municipal",
            Email = "camara@mail.com",
            Phone = "12999999999",
            Acronym = "CM",
            AgentTypeId = 1
        };

        var pa2 = new PublicAgent
        {
            Id = 12,
            Name = "Instituto Previdência Servidor Municipal",
            Email = "ipsm@mail.com",
            Phone = "12988888888",
            Acronym = "IPSM",
            AgentTypeId = 1
        };
        await _service.Add(_mapper.Map<PublicAgentDTO>(pa1));
        await _service.Add(_mapper.Map<PublicAgentDTO>(pa2));
        var agents = await _service.GetAll();
        Console.WriteLine("Agents counter : "+agents.Count());
        agents.Should().NotBeNull();
        agents.Should().NotBeEmpty();
        agents.Should().HaveCount(2);
        agents.Should().BeOfType<List<PublicAgentDTO>>();
    }

    [Fact]
    public async Task Add_Agent_OK()
    {
        var pa = new PublicAgent
        {
            Id = 15,
            Name = "Fundacao Helio Augusto de Souza",
            Email = "fundhas@mail.com",       //incorrectly formated email
            Phone = "12911111111",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };

        //Arrange
        var result = await _service.Add(_mapper.Map<PublicAgentDTO>(pa));
        var id = result.Id;
        //Act
        var result2 = await _service.GetById(id);
        //Assert
        result.Should().BeOfType<PublicAgentDTO>();
        result2.Should().BeOfType<PublicAgentDTO>();
        (result2.Id).Should().Be(id);

        Console.WriteLine("Add_Agent_OK");
        Console.WriteLine(result.Name);
        Console.WriteLine(result2.Name);

    }

    [Fact]
    public async Task Add_Agent_Already_Registered()
    {
        var pa = new PublicAgent
        {
            Id = 15,
            Name = "Fundacao Helio Augusto de Souza",
            Email = "fundhas@mail.com",       //incorrectly formated email
            Phone = "12911111111",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };
        await _service.Add(_mapper.Map<PublicAgentDTO>(pa));
        //await _service.Add(_mapper.Map<PublicAgentDTO>(pa));

        Func<Task> result = async () => await _service.Add(_mapper.Map<PublicAgentDTO>(pa));
        await result.Should().ThrowAsync<Exception>();
            
    }

    [Fact]
    protected async Task Add_Agent_PhoneNumber_Incorrectly_Formated()
    {
        var pa = new PublicAgent
        {
            Id = 47,
            Name = "ISPM",
            Email = "fundas@mail.com",       //incorrectly formated phone number
            Phone = "121",
            Acronym = "ISPM",
            AgentTypeId = 1
        };

        Func<Task> result = async () => { await _service.Add(_mapper.Map<PublicAgentDTO>(pa)); };
        await result.Should().ThrowAsync<Exception>().WithMessage("phone number must be in a valid format");
    }

    [Fact]
    protected async Task Add_Agent_Email_Incorrectly_Formated()
    {
        var pa = new PublicAgent
        {
            Id = 3,
            Name = "Mike Hawk",
            Email = "fundasmail.com",       //incorrectly formated email
            Phone = "12911111111",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };

        Func<Task> result = async () => { await _service.Add(_mapper.Map<PublicAgentDTO>(pa)); };
        await result.Should().ThrowAsync<Exception>().WithMessage("email must be in a valid format");
    }

    [Fact]
    protected async Task Update_Agent_Not_Registered()
    {
        var pa = new PublicAgent
        {
            Id = 37,      //non-existing ID
            Name = "Camara Municipal Nova",
            Email = "camara@mail.com",
            Phone = "12999999999",
            Acronym = "CM",
            AgentTypeId = 1
        };

        Func<Task> result = async () => { await _service.Update(_mapper.Map<PublicAgentDTO>(pa)); };
        await result.Should().ThrowAsync<Exception>().WithMessage("failed to update public agent. unmatched ID");
    }

    [Fact]
    protected async Task Update_Agent_PhoneNumber_Incorrectly_Formated()
    {
        var pa = new PublicAgent
        {
            Id = 999,
            Name = "ISPM",
            Email = "fundas@mail.com",   
            Phone = "121",
            Acronym = "ISPM",
            AgentTypeId = 1
        };

        Func<Task> result = async () => { await _service.Update(_mapper.Map<PublicAgentDTO>(pa)); };
        await result.Should().ThrowAsync<Exception>().WithMessage("phone number must be in a valid format");
    }

    [Fact]
    protected async Task Update_Agent_Email_Incorrectly_Formated()
    {
        var pa = new PublicAgent
        {
            Id = 9990,
            Name = "Mike Hawk",
            Email = "fundasmail.com",       //incorrectly formated email
            Phone = "12998988989",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };

        Func<Task> result = async () => { await _service.Update(_mapper.Map<PublicAgentDTO>(pa)); };
        await result.Should().ThrowAsync<Exception>().WithMessage("email must be in a valid format");
    }

    [Fact]
    protected async Task Update_OK()
    {
        var pa = new PublicAgent
        {
            Id = 667,
            Name = "Mike Hawk",
            Email = "fundas4321@mail.com",
            Phone = "12911111116",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };

        var newPa = new PublicAgent{
            Id = 667,
            Name = "New Name",
            Email = "fundas4321@mail.com",
            Phone = "12911111116",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };

        var addedAgent = await _service.Add(_mapper.Map<PublicAgentDTO>(pa));
        var idBefore = addedAgent.Id;

        await _service.Update(_mapper.Map<PublicAgentDTO>(newPa));

        var retrievedAgent = await _service.GetById(666);
        var idAfter = retrievedAgent.Id;

        idBefore.Equals(idAfter);
        retrievedAgent.Name.Equals("New Name");

    }

    [Fact]
    protected async Task Get_Agent_By_Id_OK()
    {

        var pa = new PublicAgent
        {
            Id = 100,
            Name = "Instituição Fake",
            Email = "fundas123@mail.com",
            Phone = "12911111113",
            Acronym = "FUNDHAS",
            AgentTypeId = 1
        };

        var addedAgent = await _service.Add(_mapper.Map<PublicAgentDTO>(pa));
        var agent = await _service.GetById(100);
        agent.Should().NotBeNull();
        agent.Name.Equals(addedAgent.Name);
        agent.Id.Equals(addedAgent.Id);
        agent.Email?.Equals(addedAgent.Email);
        agent.Phone?.Equals(addedAgent.Phone);
        agent.Acronym?.Equals(addedAgent.Acronym);
        agent.AgentTypeId.Equals(addedAgent.AgentTypeId);

    }
}