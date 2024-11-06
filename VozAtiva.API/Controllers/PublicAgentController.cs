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
public class PublicAgentController : ControllerBase
{
    private readonly IPublicAgentService _publicAgentService;

    public PublicAgentController(IPublicAgentService publicAgentService)
    {
        _publicAgentService = publicAgentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var publicAgents = await _publicAgentService.GetAll();
        return Ok(publicAgents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var publicAgent = await _publicAgentService.GetById(id);
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
            var createdPublicAgent = await _publicAgentService.Add(publicAgentDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPublicAgent.Id }, createdPublicAgent);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] PublicAgentDTO publicAgentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            publicAgentDto.Id = id;
            await _publicAgentService.Update(publicAgentDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _publicAgentService.Delete(id);
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
        var publicAgent = await _publicAgentService.GetByName(name);
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
            var publicAgent = await _publicAgentService.GetByEmail(email);
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
