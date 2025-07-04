using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Enums;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;

namespace Coursework.Application.Services;

public class QuestionService(
    IQuestionRepository repository,
    IAnswerRepository answerRepository,
    ITemplateRepository templateRepository) : IQuestionService
{
    public async Task<List<GetQuestionDto>> GetAllByTemplate(uint templateId)
    {
        if(!await templateRepository.Exist(templateId))
            throw new NotFoundException("Template");
            
        var questions = await repository.GetAllByTemplate(templateId);
        
        return questions.Select(QuestionMapping.ToGetQuestionDto).ToList();
    }

    public async Task<GetQuestionDto> GetById(uint id)
    {
        await Exist(id);
        
        return QuestionMapping.ToGetQuestionDto(await repository.GetById(id));
    }

    public async Task Add(AddQuestionDto question, uint templateId)
    {
        if(question == null)
            throw new InvalidInputDataException("Transmitted question can't be null");
        
        if (string.IsNullOrWhiteSpace(question.Name) ||
            string.IsNullOrWhiteSpace(question.Description) || 
            string.IsNullOrWhiteSpace(question.Type) ||
            Enum.TryParse(question.Type, out QuestionTypeEnum questionType))
            throw new InvalidDataException("Incorrect question.");
        
        if(await Exist(question.Name, question.Description, question.Type))
            throw new AlreadyAddedException("Question");
        
        if(!await templateRepository.Exist(templateId))
            throw new NotFoundException("Template");
        
        var newQuestion = QuestionMapping.FromAddQuestionDto(question);
        newQuestion.Type = questionType;
        newQuestion.TemplateId = templateId;
        
        await repository.Add(newQuestion);
    }

    public async Task Update(UpdateQuestionDto question, uint id)
    {
        if(question == null)
            throw new InvalidInputDataException("Transmitted question can't be null");
        
        if (question.Name == string.Empty || question.Description == string.Empty)
            throw new InvalidDataException("Incorrect question.");
        
        if(await Exist(question.Name, question.Description, question.Type))
            throw new AlreadyAddedException("Question with this data");

        await Exist(id);
        
        var newQuestion = QuestionMapping.FromUpdateQuestionDto(question);
        newQuestion.Type = Enum.Parse<QuestionTypeEnum>(question.Type);
        
        await repository.Update(newQuestion, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        var question = await repository.GetById(id);
        
        foreach (var questionAnswer in question.Answers)
        {
            await answerRepository.Delete(questionAnswer.Id);
        }
        
        await repository.Delete(id);
    }

    public async Task MakeDisplay(uint id)
    {
        await Exist(id);
        
        await repository.MakeDisplay(id);
    }

    public async Task MakeNotDisplay(uint id)
    {
        await Exist(id);
        
        await repository.MakeNotDisplay(id);
    }

    private async Task Exist(uint id)
    {
        if (!await repository.Exist(id))
            throw new NotFoundException("Question");
    }

    private async Task<bool> Exist(string name, string description, string type)
    {
        if(Enum.TryParse(type, out QuestionTypeEnum questionType))
            throw new InvalidInputDataException("Incorrect question type.");
        
        return await repository.Exist(name, description, questionType);
    }
}