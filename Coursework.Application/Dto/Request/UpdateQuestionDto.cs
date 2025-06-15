using Coursework.Domain.Enums;

namespace Coursework.Application.Dto.Request;

public class UpdateQuestionDto
{
    public string Name { get; set; } = string.Empty;
    public QuestionTypeEnum Type { get; set; }
    public string Description { get; set; } = string.Empty;
}