namespace Coursework.Application.Dto.Request.UpdateDtos;

public class UpdateQuestionDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; }
    public string Description { get; set; } = string.Empty;
}