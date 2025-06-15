using Coursework.Domain.Enums;

namespace Coursework.Application.Dto.Request;

public class AddQuestionDto
{
    public string Name { get; set; } = string.Empty;
    public QuestionTypeEnum Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDisplayed { get; set; } = true;
}