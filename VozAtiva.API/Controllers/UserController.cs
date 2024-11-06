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
public class UserController : ControllerBase 
{
	private readonly IUserService _userService;

	public UserController(IUserService userService) 
	{
		_userService = userService;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
	{
		var users = await _userService.GetAll();

		if (users.Count() == 0) 
		{ 
			return NotFound("Não há usuários cadastrados."); 
		}

		return Ok(users);
	}	

	[HttpGet("{id:Guid}", Name = "GetUserById")]
	public async Task<ActionResult<UserDTO>> GetById(Guid id)
	{
		var user = await _userService.GetById(id);

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

		await _userService.Add(userDto);

		return Ok("Usuário registrado com sucesso!");
	}

	[HttpPut("{id:Guid}")]
	public async Task<ActionResult> Update(Guid id, UserDTO userDto)
	{
		if (id != userDto.Id) 
		{ 
			return BadRequest(); 
		}

		await _userService.Update(userDto);

		return Ok(userDto);
	}

	[HttpDelete("{id:Guid}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		var user = await _userService.GetById(id);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		await _userService.Delete(id);

		return Ok("Usuário removido com sucesso!");
	}

	[HttpGet("{email}")]
	public async Task<ActionResult<UserDTO>> GetByEmail(string email)
	{
		var user = await _userService.GetByEmail(email);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}

	[HttpGet("{federalCodeClient}")]
	public async Task<ActionResult<UserDTO>> GetByFederalCodeClient(string federalCodeClient)
	{
		var user = await _userService.GetByEmail(federalCodeClient);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}

	[HttpGet("{phone}")]
	public async Task<ActionResult<UserDTO>> GetByPhone(string phone)
	{
		var user = await _userService.GetByEmail(phone);

		if (user is null) 
		{ 
			return NotFound("Usuário não encontrado."); 
		}

		return Ok(user);
	}
}
