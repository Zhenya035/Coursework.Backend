using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Coursework.Infrastructure.Jwt;

public class JwtService(IOptions<AuthSettings> authSettings) : IJwtService
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("email", user.Email),
            new("role", user.Role.ToString()),
            new("status", user.Status.ToString())
        };
        
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(authSettings.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(authSettings.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}