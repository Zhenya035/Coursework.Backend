namespace Coursework.Domain.Models;

public class Form
{
    public uint Id { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public List<Answer> Answers { get; set; } = [];
    
    public Template? Template { get; set; }
    public uint TemplateId { get; set; }
    
    public User? Author { get; set; }
    public uint? AuthorId { get; set; }
}