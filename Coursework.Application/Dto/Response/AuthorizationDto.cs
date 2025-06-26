namespace Coursework.Application.Dto.Response;

public class AuthorizationDto
{
    public uint Id { get; set; } = 0;
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}