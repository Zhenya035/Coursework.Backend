namespace Coursework.Application.Dto.Response;

public class GetQuestionDto
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDisplayed { get; set; } = true;
    public List<GetAnswerDto> Answers { get; set; } = [];
    public string TemplateTitle { get; set; } = string.Empty;
}