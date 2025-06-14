namespace Coursework.Domain.Models;

public class Answer
{
    public uint Id { get; set; }
    public string Value { get; set; } = string.Empty;
    
    public Form? Form { get; set; }
    public uint FormId { get; set; }
    
    public Question? Question { get; set; }
    public uint? QuestionId { get; set; }
}