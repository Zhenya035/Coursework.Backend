namespace Coursework.Domain.Models;

public class Like
{
    public uint Id { get; set; }
    
    public User? Author { get; set; }
    public uint AuthorId { get; set; }
    
    public Template? Template { get; set; }
    public uint TemplateId { get; set; }
}