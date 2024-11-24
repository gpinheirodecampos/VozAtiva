using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Application.DTOs;
using VozAtiva.API.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PublicAgentControllerTests
{
    private readonly Mock<IPublicAgentService> _mockService;
    private readonly PublicAgentController _controller;

    public PublicAgentControllerTests()
    {
        _mockService = new Mock<IPublicAgentService>();
        _controller = new PublicAgentController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkResultWithList()
    {
        // Arrange
        var agents = new List<PublicAgentDTO>
        {
            new PublicAgentDTO( Id: 1,
                    Name: "Agent 1",
                    Email: "agent1@example.com",
                    Phone: "555-1234",
                    Acronym: "AG1",
                    AgentTypeId: 101
                ),
            new PublicAgentDTO(
                Id: 2,
                Name: "Agent 2",
                Email: "agent2@example.com",
                Phone: "555-5678",
                Acronym: "AG2",
                AgentTypeId: 102
            )

        };

        _mockService.Setup(service => service.GetAll()).ReturnsAsync(agents);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<PublicAgentDTO>>(okResult.Value);
        Assert.Equal(agents.Count, ((List<PublicAgentDTO>)returnValue).Count);
    }

    [Fact]
    public async Task GetById_ShouldReturnOkResult_WhenAgentExists()
    {
        // Arrange
        var agent = new PublicAgentDTO( Id: 1,
                    Name: "Agent 1",
                    Email: "agent1@example.com",
                    Phone: "555-1234",
                    Acronym: "AG1",
                    AgentTypeId: 101
                );

        _mockService.Setup(service => service.GetById(1)).ReturnsAsync(agent);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PublicAgentDTO>(okResult.Value);
        Assert.Equal(agent.Name, returnValue.Name);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenAgentDoesNotExist()
    {
        // Arrange
        _mockService.Setup(service => service.GetById(1))
            .ReturnsAsync((PublicAgentDTO)null!);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Post_ShouldReturnCreatedAtActionResult_WhenAgentIsCreated()
    {
        // Arrange
        var newAgent = new PublicAgentDTO( Id: 1,
                    Name: "Agent 1",
                    Email: "agent1@example.com",
                    Phone: "555-1234",
                    Acronym: "AG1",
                    AgentTypeId: 101
                );

        _mockService.Setup(service => service.Add(It.IsAny<PublicAgentDTO>())).ReturnsAsync(newAgent);

        // Act
        var result = await _controller.Post(newAgent);

        // Assert
        var createdAtResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<PublicAgentDTO>(createdAtResult.Value);
        Assert.Equal(newAgent.Name, returnValue.Name);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Name is required");

        var newAgent = new PublicAgentDTO( Id: 1,
                    Name: "Agent 1",
                    Email: "agent1@example.com",
                    Phone: "555-1234",
                    Acronym: "AG1",
                    AgentTypeId: 101
                );

        // Act
        var result = await _controller.Post(newAgent);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenAgentIsDeleted()
    {
        // Arrange
        _mockService.Setup(service => service.Delete(It.IsAny<Guid>())).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        _mockService.Setup(service => service.Delete(It.IsAny<Guid>())).ThrowsAsync(new Exception("Error deleting agent"));

        // Act
        var result = await _controller.Delete(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var message = Assert.IsAssignableFrom<dynamic>(badRequestResult.Value);
        Assert.Equal("Error deleting agent", message.message);
    }
}
