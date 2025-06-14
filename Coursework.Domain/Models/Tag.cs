namespace Coursework.Domain.Models;

public class Tag
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TemplatesTags> Templates { get; set; } = [];
}