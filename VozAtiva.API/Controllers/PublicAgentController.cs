using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Application.DTOs;

namespace VozAtiva.API.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class PublicAgentController(IPublicAgentService publicAgentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var publicAgents = await publicAgentService.GetAll();
        return Ok(publicAgents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var publicAgent = await publicAgentService.GetById(id);
        if (publicAgent == null)
        {
            return NotFound();
        }
        return Ok(publicAgent);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PublicAgentDTO publicAgentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdPublicAgent = await publicAgentService.Add(publicAgentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPublicAgent.Id }, createdPublicAgent);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PublicAgentDTO publicAgentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await publicAgentService.Update(publicAgentDto);
            return Ok("Órgão Público atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var agent = await publicAgentService.GetById(id);

            if (agent == null)
            {
                return NotFound();
            }

            await publicAgentService.Delete(agent);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var publicAgent = await publicAgentService.GetByName(name);
        if (publicAgent == null)
        {
            return NotFound();
        }
        return Ok(publicAgent);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            var publicAgent = await publicAgentService.GetByEmail(email);
            if (publicAgent == null)
            {
                return NotFound();
            }
            return Ok(publicAgent);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
