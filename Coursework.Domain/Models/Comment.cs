namespace Coursework.Domain.Models;

public class Comment
{
    public uint Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public User? Author { get; set; }
    public uint? AuthorId { get; set; }
    
    public Template? Template { get; set; }
    public uint TemplateId { get; set; }
}