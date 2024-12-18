using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VozAtiva.Application.DTOs;
using VozAtiva.Application.Services.Interfaces;
using VozAtiva.Domain.Entities;

namespace VozAtiva.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email!);

        if (user == null)
        {
            return Unauthorized();
        }

        var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

        if (!passwordValid)
        {
            return Unauthorized();
        }

        var userRoles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Email, user.Email!)
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var accessToken = tokenService.GenerateAccessToken(claims, configuration);

        var refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;

        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(Double.Parse(configuration["JWT:RefreshTokenValidityInMinutes"]));

        await userManager.UpdateAsync(user);

        return Ok(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken = refreshToken,
            Expiration = accessToken.ValidTo
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await userManager.FindByEmailAsync(model.Email!);

        if (userExists != null)
        {
            return BadRequest("User already exists!");
        }

        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Username,
        };

        var result = await userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenModel model)
    {
        if (model is null)
        {
            return BadRequest("Invalid client request");
        }

        string? accessToken = model.AccessToken ?? throw new Exception("Access token is missing");

        string? refreshToken = model.RefreshToken ?? throw new Exception("Refresh token is missing");

        var principal = tokenService.GetPrincipalFromExpiredToken(accessToken, configuration);

        if (principal is null)
        {
            return BadRequest("Invalid access token/refresh token");
        }

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var user = await userManager.FindByIdAsync(userId);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return BadRequest("User not found");
        }

        var newAccessToken = tokenService.GenerateAccessToken(principal.Claims.ToList(), configuration);

        var newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(Double.Parse(configuration["JWT:RefreshTokenValidityInMinutes"]));

        await userManager.UpdateAsync(user);

        return Ok(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
            Expiration = newAccessToken.ValidTo
        });
    }

    [HttpPost("revoke/{email}")]
    public async Task<IActionResult> Revoke(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return BadRequest("User not found");
        }

        user.RefreshToken = null;

        user.RefreshTokenExpiryTime = null;

        await userManager.UpdateAsync(user);

        return Ok();
    }
}
