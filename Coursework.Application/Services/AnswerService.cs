using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class AnswerService(
    IAnswerRepository repository,
    IFormRepository formRepository,
    IQuestionRepository questionRepository) : IAnswerService
{
    public async Task<List<GetAnswerDto>> GetAllByForm(uint formId)
    {
        if(!await formRepository.Exist(formId))
            throw new NotFoundException("Form");
        
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

    public async Task Create(AddAnswerDto answer, uint questionId)
    {
        if (answer == null)
            throw new InvalidInputDataException("Comment content can't be null.");
        
        if (answer.Value == string.Empty)
            throw new InvalidInputDataException("Comment content can't be empty.");
        
        if(!await questionRepository.Exist(questionId))
            throw new NotFoundException("Question");
        
        var newAnswer = AnswerMapping.FromAddAnswerDto(answer);
        newAnswer.QuestionId = questionId;
        
        await repository.Create(newAnswer);
    }

    public async Task Update(UpdateAnswerDto newValue, uint id)
    {
        if(newValue.Value == string.Empty)
            throw new InvalidDataException("Incorrect answer value");

        await Exist(id);
        
        await repository.Update(newValue.Value, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    private async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Answer");
    }
}