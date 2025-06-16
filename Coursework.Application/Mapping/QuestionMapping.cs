using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class QuestionMapping
{
    public static Question FromAddQuestionDto(AddQuestionDto addQuestionDto) =>
        new Question()
        {
            Name = addQuestionDto.Name,
            Type = addQuestionDto.Type,
            Description = addQuestionDto.Description,
            IsDisplayed = addQuestionDto.IsDisplayed
        };
    
    public static Question FromUpdateQuestionDto(UpdateQuestionDto updateQuestionDto) =>
        new Question()
        {
            Name = updateQuestionDto.Name,
            Type = updateQuestionDto.Type,
            Description = updateQuestionDto.Description
        };

    public static GetQuestionDto ToGetQuestionDto(this Question question) =>
        new GetQuestionDto()
        {
            Id = question.Id,
            Name = question.Name,
            Type = question.Type.ToString(),
            Description = question.Description,
            IsDisplayed = question.IsDisplayed,
            Answers = question.Answers.Select(AnswerMapping.ToGetAnswerDto).ToList(),
            TemplateTitle = question.Template.Title
        };
}