namespace Coursework.Application.Dto.Response;

public class GetFormDto
{
    public uint Id { get; set; }
    public List<GetAnswerDto> Answers { get; set; } = [];
    public string TemplateTitle { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}