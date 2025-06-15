namespace Coursework.Application.Dto.Response;

public class GetCommentDto
{
    public uint Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}