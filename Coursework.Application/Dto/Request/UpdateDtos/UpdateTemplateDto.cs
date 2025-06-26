namespace Coursework.Application.Dto.Request.UpdateDtos;

public class UpdateTemplateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    
    public List<UpdateQuestionDto> Questions { get; set; } = [];
}