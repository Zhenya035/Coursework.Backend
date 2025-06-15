namespace Coursework.Application.Dto.Response;

public class GetLikeDto
{
    public uint Id { get; set; }
    public string TemplateTitle { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
}