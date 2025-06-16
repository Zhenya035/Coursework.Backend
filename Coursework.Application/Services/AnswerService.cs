using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class AnswerService(
    IAnswerRepository repository,
    IFormService formService,
    IQuestionService questionService) : IAnswerService
{
    public async Task<List<GetAnswerDto>> GetAllByForm(uint formId)
    {
        await formService.Exist(formId);
        
        var answers = await repository.GetAllByForm(formId);
        return answers.Select(AnswerMapping.ToGetAnswerDto).ToList();
    }

    public async Task<GetAnswerDto> GetById(uint id)
    {
        await Exist(id);
        
        return AnswerMapping.ToGetAnswerDto(await repository.GetById(id));
    }
    
    public async Task<Answer> GetByIdWithoutMapping(uint id)
    {
        await Exist(id);
        
        return await repository.GetById(id);
    }

    public async Task Create(AddAnswerDto answer, uint formId, uint questionId)
    {
        if (answer == null)
            throw new InvalidInputDataException("Comment content can't be null.");
        
        if (answer.Value == string.Empty)
            throw new InvalidInputDataException("Comment content can't be empty.");

        await formService.Exist(formId);
        await questionService.Exist(questionId);
        
        var newAnswer = AnswerMapping.FromAddAnswerDto(answer);
        newAnswer.FormId = formId;
        newAnswer.QuestionId = questionId;
        
        await repository.Create(newAnswer);
    }

    public async Task Update(string newValue, uint id)
    {
        if(newValue == string.Empty)
            throw new InvalidDataException("Incorrect answer value");

        await Exist(id);
        
        await repository.Update(newValue, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Answer");
    }
}