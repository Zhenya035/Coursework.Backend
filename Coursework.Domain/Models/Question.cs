using Coursework.Domain.Enums;

namespace Coursework.Domain.Models;

public class Question
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public QuestionTypeEnum Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDisplayed { get; set; } = true;
}