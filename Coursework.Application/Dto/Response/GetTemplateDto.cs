namespace Coursework.Application.Dto.Response;

public class GetTemplateDto
{
    public uint Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<string> AuthorisedEmails { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<string> Tags { get; set; } = [];
    public int LikesCount { get; set; } = 0;
    public int QuestionsCount { get; set; } = 0;
    public List<GetCommentDto> Comments { get; set; } = [];
    public int FormsCount { get; set; } = 0;
    public string Author { get; set; } = string.Empty;
}