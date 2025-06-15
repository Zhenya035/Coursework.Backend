namespace Coursework.Domain.Models;

public class Template
{
    public uint Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<string> AuthorisedEmails { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public List<TemplatesTags> Tags { get; set; } = [];
    
    public List<Like> Likes { get; set; } = [];
    
    public List<Comment> Comments { get; set; } = [];
    
    public List<Form> Forms { get; set; } = [];
    
    public User? Author { get; set; }
    public uint AuthorId { get; set; }
}