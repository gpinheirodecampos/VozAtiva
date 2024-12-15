using Microsoft.AspNetCore.Mvc;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace VozAtiva.API.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class AlertController(IAlertService alertService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlertDTO>>> GetAll()
    {
        var alerts = await alertService.GetAll();

        if (!alerts.Any())
        {
            return NotFound("Nenhum alerta encontrado.");
        }
        
        return Ok(alerts);
    } 

    [HttpGet("{id:int}", Name = "GetAlertById")]
    public async Task<ActionResult<AlertDTO>> GetById(Guid id)
    {
        var alert = await alertService.GetById(id);

        if (alert is null)
        {
            return NotFound("Alerta não encontrado.");
        }

        return Ok(alert);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AlertDTO alertDto)
    {
        if (alertDto is null)
        {
            return BadRequest("Body não informado.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdAlert = await alertService.Add(alertDto);
            return Ok("Alerta criado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] AlertDTO alertDto)
    {
        if (alertDto is null)
        {
            return BadRequest("Body não informado.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await alertService.Update(alertDto);
            return Ok("Alerta atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var alert = await alertService.GetById(id);

        if (alert is null)
        {
            return NotFound("Alerta não encontrado.");
        }

        try
        {
            await alertService.Delete(alert);
            return Ok("Alerta removido com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("title/{title}")]
    public async Task<ActionResult<AlertDTO>> GetByTitle(string title)
    {
        var alert = await alertService.GetByTitle(title);

        if (alert is null)
        {
            return NotFound("Nenhum alerta encontrado com este título.");
        }

        return Ok(alert);
    }

    [HttpGet("date/{date:datetime}")]
    public async Task<ActionResult<IEnumerable<AlertDTO>>> GetByDate(DateTime date)
    {
        var alerts = await alertService.GetByDate(date);

        if (!alerts.Any())
        {
            return NotFound("Nenhum alerta encontrado para esta data.");
        }

        return Ok(alerts);
    }
}
