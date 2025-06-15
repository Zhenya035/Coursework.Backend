namespace Coursework.Application.Dto.Request;

public class AddFormDto
{
    public uint Id { get; set; }
    public List<uint> AnswerIds { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}