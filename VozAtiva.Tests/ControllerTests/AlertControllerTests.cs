using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VozAtiva.API.Controllers;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Domain.Enums;
using Xunit;

public class AlertControllerTests
{
    private readonly Mock<IAlertService> _mockAlertService;
    private readonly AlertController _controller;

    public AlertControllerTests()
    {
        _mockAlertService = new Mock<IAlertService>();
        _controller = new AlertController(_mockAlertService.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WhenAlertsExist()
    {
        var alerts = new List<AlertDTO>
        {
            new (Guid.NewGuid(),"Alert 1", "This is the description for Alert 1", DateTime.Now, Guid.NewGuid(), 1, 1, 1.03, 2.05, VozAtiva.Domain.Enums.AlertStatusEnum.Pending),
            new (Guid.NewGuid(),"Alert 2", "This is the description for Alert 2", DateTime.Now, Guid.NewGuid(), 1, 1, 1.03, 2.05, VozAtiva.Domain.Enums.AlertStatusEnum.Pending)
        };
        _mockAlertService.Setup(s => s.GetAll()).ReturnsAsync(alerts);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAlerts = Assert.IsType<List<AlertDTO>>(okResult.Value);
        Assert.Equal(2, returnedAlerts.Count);
    }

    [Fact]
    public async Task GetAll_ShouldReturnNotFound_WhenNoAlertsExist()
    {
        _mockAlertService.Setup(s => s.GetAll()).ReturnsAsync(new List<AlertDTO>());

        var result = await _controller.GetAll();

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenAlertExists()
    {
        var alertId = Guid.NewGuid();
        var alert = new AlertDTO( alertId, "Alert 1", "This is the description for Alert 1", DateTime.Now, Guid.NewGuid(), 1, 1, 13, 75, AlertStatusEnum.Pending);
        _mockAlertService.Setup(s => s.GetById(alertId)).ReturnsAsync(alert);

        var result = await _controller.GetById(alertId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAlert = Assert.IsType<AlertDTO>(okResult.Value);
        Assert.Equal(alertId, returnedAlert.Id);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenAlertDoesNotExist()
    {
        var alertId = Guid.NewGuid();
        _mockAlertService.Setup(s => s.GetById(alertId)).ReturnsAsync((AlertDTO)null);

        var result = await _controller.GetById(alertId);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated_WhenAlertIsCreated()
    {
        var alertDto = new AlertDTO(Guid.NewGuid(), "Alert 1", "This is the description for Alert 1", DateTime.Now, Guid.NewGuid(), 1, 1, 16 , 14, AlertStatusEnum.Pending);
        _mockAlertService.Setup(s => s.Add(alertDto)).ReturnsAsync(alertDto);

        var result = await _controller.Create(alertDto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedAlert = Assert.IsType<AlertDTO>(createdResult.Value);
        Assert.Equal(alertDto.Id, returnedAlert.Id);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenAlertDtoIsNull()
    {
        var result = await _controller.Create(null);

        Assert.IsType<BadRequestObjectResult>(result);
    }


    [Fact]
    public async Task GetByTitle_ReturnsAlert_WhenAlertExist()
    {
        var title = "Test Alert";
        var alert = new AlertDTO(Guid.NewGuid(), title, "Description 1", DateTime.Now, Guid.NewGuid(), 1, 1, 18, 19, AlertStatusEnum.Pending);

        _mockAlertService.Setup(s => s.GetByTitle(title)).ReturnsAsync(alert);

        var result = await _controller.GetByTitle(title);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAlert = Assert.IsType<AlertDTO>(okResult.Value);
        Assert.Equal(title, returnedAlert.Title);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenUpdateIsSuccessful()
    {
        var alertToUpdate = new AlertDTO(Guid.NewGuid(), "Alert 1", "Description 1", DateTime.Now, Guid.NewGuid(), 1, 1, 25.9, 23.1, AlertStatusEnum.Pending);

        _mockAlertService.Setup(s => s.Update(alertToUpdate)).Returns(Task.CompletedTask);

        var result = await _controller.Update(alertToUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Alerta atualizado com sucesso!", okResult.Value);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenBodyIsNull()
    {
        var result = await _controller.Update(null);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Body não informado.", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenDeleteIsSuccessful()
    {
        var alertId = Guid.NewGuid();
        var alert = new AlertDTO(alertId, "Alert 1", "Description 1", DateTime.Now, Guid.NewGuid(), 1, 1, 45.3 , 93.2 ,AlertStatusEnum.Pending);

        _mockAlertService.Setup(s => s.GetById(alertId)).ReturnsAsync(alert);
        _mockAlertService.Setup(s => s.Delete(alert)).Returns(Task.CompletedTask);

        var result = await _controller.Delete(alertId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Alerta removido com sucesso!", okResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenAlertDoesNotExist()
    {
        var alertId = Guid.NewGuid();
        _mockAlertService.Setup(s => s.GetById(alertId)).ReturnsAsync((AlertDTO)null);

        var result = await _controller.Delete(alertId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Alerta não encontrado.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetByTitle_ReturnsNotFound_WhenNoAlertsExist()
    {
        var title = "Nonexistent Title";
        _mockAlertService.Setup(s => s.GetByTitle(title)).ReturnsAsync((AlertDTO)null);

        var result = await _controller.GetByTitle(title);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetByDate_ReturnsAlerts_WhenAlertsExist()
    {
        var date = DateTime.Now.Date;
        var alerts = new List<AlertDTO>
        {
            new AlertDTO(Guid.NewGuid(), "Alert 1", "Description 1", date, Guid.NewGuid(), 1, 1, 48.7, 63.8 , AlertStatusEnum.Pending),
            new AlertDTO(Guid.NewGuid(), "Alert 2", "Description 2", date, Guid.NewGuid(), 1, 1, 41.2, 38.9, AlertStatusEnum.Pending)
        };

        _mockAlertService.Setup(s => s.GetByDate(date)).ReturnsAsync(alerts);

        var result = await _controller.GetByDate(date);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedAlerts = Assert.IsType<List<AlertDTO>>(okResult.Value);
        Assert.Equal(2, returnedAlerts.Count);
    }

    [Fact]
    public async Task GetByDate_ReturnsNotFound_WhenNoAlertsExist()
    {
        var date = DateTime.Now.Date;
        _mockAlertService.Setup(s => s.GetByDate(date)).ReturnsAsync(Enumerable.Empty<AlertDTO>());

        var result = await _controller.GetByDate(date);

        Assert.IsType<NotFoundObjectResult>(result.Result);
    }


}