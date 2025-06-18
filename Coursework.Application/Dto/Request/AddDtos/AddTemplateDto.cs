namespace Coursework.Application.Dto.Request.AddDtos;

public class AddTemplateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<string> AuthorisedEmails { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    
    public List<AddQuestionDto> Questions { get; set; } = [];
}