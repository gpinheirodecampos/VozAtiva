using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VozAtiva.API.Controllers;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;
using Xunit;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

     [Fact]
    public async Task GetAll_ReturnsOk_WhenUsersExist()
    {
        var users = new List<UserDTO>
        {
            new UserDTO(Guid.NewGuid(), "Usuaio 1", "email@email.com", "11122233345", DateTime.Now, "11912345678", VozAtiva.Domain.Enums.UserTypeEnum.User),
            new UserDTO(Guid.NewGuid(), "Usuaio 2", "email2@email.com", "11122233346", DateTime.Now, "11912345679", VozAtiva.Domain.Enums.UserTypeEnum.User)
        };
        _mockUserService.Setup(s => s.GetAll()).ReturnsAsync(users);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<UserDTO>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count);
    }

     [Fact]
    public async Task GetAll_ReturnsNotFound_WhenNoUsersExist()
    {
        _mockUserService.Setup(s => s.GetAll()).ReturnsAsync(new List<UserDTO>());

        var result = await _controller.GetAll();

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenUserExists()
    {
        var userId = Guid.NewGuid();
        var user = new UserDTO(userId, "Usuaio 1", "email@email.com", "11122233345", DateTime.Now, "11912345678", VozAtiva.Domain.Enums.UserTypeEnum.User);
        _mockUserService.Setup(s => s.GetById(userId)).ReturnsAsync(user);

        var result = await _controller.GetById(userId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
        Assert.Equal(userId, returnedUser.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockUserService.Setup(s => s.GetById(userId)).ReturnsAsync((UserDTO)null);

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

     [Fact]
    public async Task Create_ReturnsOk_WhenUserIsCreated()
    {
        // Arrange
        var userDto = new UserDTO(Guid.NewGuid(), "Usuaio 1", "email@email.com", "11122233345", DateTime.Now, "11912345678", VozAtiva.Domain.Enums.UserTypeEnum.User);
        _mockUserService.Setup(s => s.Add(It.IsAny<UserDTO>())).ReturnsAsync(userDto);

        // Act
        var result = await _controller.Create(userDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Usuário registrado com sucesso!", okResult.Value);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var userDto = new UserDTO(Guid.NewGuid(), "Usuaio 1", "email@email.com", "11122233345", DateTime.Now, "11912345678", VozAtiva.Domain.Enums.UserTypeEnum.User);
        _mockUserService.Setup(s => s.Add(It.IsAny<UserDTO>())).ThrowsAsync(new Exception("Some error"));

        // Act
        var result = await _controller.Create(userDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsAssignableFrom<object>(badRequestResult.Value);
        Assert.NotNull(errorResponse); // Confirma que há alguma mensagem de erro, mas não verifica o conteúdo exato
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenUserIsUpdated()
    {
        // Arrange
        var userDto = new UserDTO(Guid.NewGuid(), "Usuaio 1", "email@email.com", "11122233345", DateTime.Now, "11912345678", VozAtiva.Domain.Enums.UserTypeEnum.User);
        _mockUserService.Setup(s => s.Update(userDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(userDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(userDto, okResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenUserIsDeleted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new UserDTO(userId, "Usuaio 1", "email@email.com", "11122233345", DateTime.Now, "11912345678", VozAtiva.Domain.Enums.UserTypeEnum.User);
        _mockUserService.Setup(s => s.GetById(userId)).ReturnsAsync(user);
        _mockUserService.Setup(s => s.Delete(user)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Usuário removido com sucesso!", okResult.Value);
    }


    [Fact]
    public async Task Delete_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _mockUserService.Setup(s => s.GetById(userId)).ReturnsAsync((UserDTO)null);

        // Act
        var result = await _controller.Delete(userId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }



}