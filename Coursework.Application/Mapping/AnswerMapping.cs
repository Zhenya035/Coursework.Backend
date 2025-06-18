using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class AnswerMapping
{
    public static GetAnswerDto ToGetAnswerDto(Answer answer) =>
        new GetAnswerDto()
        {
            Id = answer.Id,
            QuestionName = answer.Question.Name,
            QuestionDescription = answer.Question.Description,
            Value = answer.Value,
        };
}