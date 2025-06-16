using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class FormService(
    IFormRepository repository,
    ITemplateService templateService,
    IUserService userService,
    IAnswerService answerService) : IFormService
{
    public async Task<List<GetFormDto>> GetAllByTemplate(uint templateId)
    {
        await templateService.Exist(templateId);
        
        var forms = await repository.GetAllByTemplate(templateId);
        
        return forms.Select(FormMapping.ToGetFormDto).ToList();
    }

    public async Task<GetFormDto> GetById(uint id)
    {
        await Exist(id);
        
        return FormMapping.ToGetFormDto(await repository.GetById(id));
    }

    private async Task<Form> GetByIdWithoutMapping(uint id)
    {
        await Exist(id);
        
        return await repository.GetById(id);
    }

    public async Task Fill(AddFormDto form, uint templateId, uint authorId)
    {
        if(form == null)
            throw new InvalidInputDataException("Transmitted form can't be null");
        
        if (form.AnswerIds.Count == 0)
            throw new InvalidInputDataException("Answer Ids are required");
        
        await templateService.Exist(templateId);
        await userService.Exist(authorId);

        var answers = new List<Answer>();
        
        foreach (var answerId in form.AnswerIds)
        {
            answers.Add(await answerService.GetByIdWithoutMapping(answerId));
        }
        
        var newForm = FormMapping.FromAddFormDto(answers);
        newForm.TemplateId = templateId;
        newForm.AuthorId = authorId;
        newForm.CreatedAt = DateTime.UtcNow;
        
        await repository.Fill(newForm);
    }

    public async Task Edit(List<uint> answerIds, uint id)
    {
        if (answerIds.Count == 0)
            throw new InvalidInputDataException("Answer Ids are required");
        
        var answers = new List<Answer>();
        
        foreach (var answerId in answerIds)
        {
            answers.Add(await answerService.GetByIdWithoutMapping(answerId));
        }
        
        var form = await GetByIdWithoutMapping(id);
        
        await repository.Edit(form, answers);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Form");
    }
}