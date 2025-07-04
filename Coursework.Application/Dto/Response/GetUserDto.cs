namespace Coursework.Application.Dto.Response;

public class GetUserDto
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int TemplatesCount { get; set; } = 0;
}   