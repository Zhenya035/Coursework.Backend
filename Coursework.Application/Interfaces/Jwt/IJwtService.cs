using Coursework.Domain.Models;

namespace Coursework.Application.Interfaces.Jwt;

public interface IJwtService
{
    public string GenerateToken(User user);
}