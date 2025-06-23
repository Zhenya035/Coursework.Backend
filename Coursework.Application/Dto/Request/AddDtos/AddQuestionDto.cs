namespace Coursework.Application.Dto.Request.AddDtos;

public class AddQuestionDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDisplayed { get; set; } = true;
}