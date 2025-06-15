namespace Coursework.Application.Dto.Response;

public class GetAnswerDto
{
    public uint Id { get; set; }
    public string QuestionName { get; set; } = string.Empty;
    public string QuestionDescription { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}