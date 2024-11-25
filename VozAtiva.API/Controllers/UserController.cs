using Microsoft.AspNetCore.Mvc;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Application.DTOs;

namespace VozAtiva.API.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class UserController(IUserService userService) : ControllerBase 
{
    [HttpGet]
	public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
	{
		var users = await userService.GetAll();

		if (!users.Any()) 
		{ 
			return NotFound("Não há usuários cadastrados."); 
		}

		return Ok(users);
	}	

	[HttpGet("{id:Guid}", Name = "GetUserById")]
	public async Task<ActionResult<UserDTO>> GetById(Guid id)
	{
		var user = await userService.GetById(id);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}

	[HttpPost]
	public async Task<ActionResult> Create(UserDTO userDto)
	{
		if (userDto is null) 
		{ 
			return BadRequest("Body não informado."); 
		}

		try
		{
            await userService.Add(userDto);

            return Ok("Usuário registrado com sucesso!");
        }
		catch(Exception ex)
		{
            return BadRequest(new { message = ex.Message });
        }
	}

	[HttpPut]
	public async Task<ActionResult> Update(UserDTO userDto)
	{
		await userService.Update(userDto);

		return Ok(userDto);
	}

	[HttpDelete("{id:Guid}")]
	public async Task<ActionResult> Delete(Guid id)
	{
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await userService.GetById(id);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		try
		{
            await userService.Delete(user);
            return Ok("Usuário removido com sucesso!");
        }
		catch (Exception ex)
		{
			return BadRequest(new { message = ex.Message });
        }		
	}

	[HttpGet("email/{email}")]
	public async Task<ActionResult<UserDTO>> GetByEmail(string email)
	{
		var user = await userService.GetByEmail(email);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}

	[HttpGet("document/{federalCodeClient}")]
	public async Task<ActionResult<UserDTO>> GetByFederalCodeClient(string federalCodeClient)
	{
		var user = await userService.GetByEmail(federalCodeClient);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}

	[HttpGet("phone/{phone}")]
	public async Task<ActionResult<UserDTO>> GetByPhone(string phone)
	{
		var user = await userService.GetByEmail(phone);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}
}
