namespace Coursework.Domain.Models;

public class TemplatesTags
{
    public uint Id { get; set; }
    
    public Template? Template { get; set; }
    public uint TemplateId { get; set; }
    
    public Tag? Tag { get; set; }
    public uint TagId { get; set; }
}