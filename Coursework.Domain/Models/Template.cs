namespace Coursework.Domain.Models;

public class Template
{
    public uint Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    public List<User> AuthorisedUsers { get; set; } = [];
    public List<Like> Likes { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public User? Author { get; set; }
    public uint AuthorId { get; set; }
}