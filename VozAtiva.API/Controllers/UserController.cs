using VozAtiva.Application.Services.Interfaces;

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
}
