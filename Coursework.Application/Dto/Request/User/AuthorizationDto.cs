namespace Coursework.Application.Dto.Request.User;

public class AuthorizationDto
{
    public uint Id { get; set; } = 0;
    public string Token { get; set; } = string.Empty;
}