using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VozAtiva.API.Controllers;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;
using Xunit;

public class PublicAgentControllerTests
{
    private readonly Mock<IPublicAgentService> _publicAgentServiceMock;
    private readonly PublicAgentController _controller;

    public PublicAgentControllerTests()
    {
        _publicAgentServiceMock = new Mock<IPublicAgentService>();
        _controller = new PublicAgentController(_publicAgentServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfPublicAgents()
    {
        // Arrange
        var publicAgents = new List<PublicAgentDTO>
        {
            new PublicAgentDTO (1, "PublicAgent1", "public@agent.com", "1111-1111", "ORG1", 1),
            new PublicAgentDTO (2, "PublicAgent2", "public2@agent.com", "2222-2222", "ORG2", 1)
        };
        _publicAgentServiceMock.Setup(s => s.GetAll()).ReturnsAsync(publicAgents);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedAgents = Assert.IsAssignableFrom<IEnumerable<PublicAgentDTO>>(okResult.Value);
        Assert.Equal(publicAgents.Count, ((List<PublicAgentDTO>)returnedAgents).Count);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenPublicAgentExists()
    {
        // Arrange
        var publicAgent = new PublicAgentDTO (1, "PublicAgent1", "public@agent.com", "1111-1111", "ORG1", 1);
        _publicAgentServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(publicAgent);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(publicAgent, okResult.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenPublicAgentDoesNotExist()
    {
        // Arrange
        _publicAgentServiceMock.Setup(s => s.GetById(1)).ReturnsAsync((PublicAgentDTO)null);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreatedResult_WhenPublicAgentIsCreated()
    {
        // Arrange
        var publicAgent = new PublicAgentDTO (1, "PublicAgent1", "public@agent.com", "1111-1111", "ORG1", 1);
        _publicAgentServiceMock.Setup(s => s.Add(publicAgent)).ReturnsAsync(publicAgent);

        // Act
        var result = await _controller.Post(publicAgent);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(publicAgent, createdResult.Value);
    }

    [Fact]
    public async Task Post_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Required");

        // Act
        var result = await _controller.Post(new PublicAgentDTO(0, null, null, null, null, 0));

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenPublicAgentIsDeleted()
    {
        // Arrange
        var publicAgent = new PublicAgentDTO (1, "PublicAgent1", "public@agent.com", "1111-1111", "ORG1", 1);
        _publicAgentServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(publicAgent);
        _publicAgentServiceMock.Setup(s => s.Delete(publicAgent)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenPublicAgentDoesNotExist()
    {
        // Arrange
        _publicAgentServiceMock.Setup(s => s.GetById(1)).ReturnsAsync((PublicAgentDTO)null);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
