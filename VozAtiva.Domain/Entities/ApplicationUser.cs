using Microsoft.AspNetCore.Identity;

namespace VozAtiva.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
