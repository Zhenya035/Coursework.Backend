using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class FormMapping
{
    public static Form FromAddFormDto(List<Answer> answers) =>
        new Form()
        {
            Answers = answers,
            CreatedAt = DateTime.UtcNow
        };

    public static GetFormDto ToGetFormDto(Form form) =>
        new GetFormDto()
        {
            Id = form.Id,
            Answers = form.Answers.Select(AnswerMapping.ToGetAnswerDto).ToList(),
            TemplateTitle = form.Template.Title,
            Author = form.Author.Name,
            CreatedAt = form.CreatedAt,
        };
}