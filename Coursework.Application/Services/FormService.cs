using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class FormService(
    IFormRepository repository,
    ITemplateRepository templateRepository,
    IUserRepository userRepository,
    IAnswerRepository answerRepository) : IFormService, IOwnerService<Form>
{
    public async Task<List<GetFormDto>> GetAllByTemplate(uint templateId)
    {
        if(!await templateRepository.Exist(templateId))
            throw new NotFoundException("Template");
        
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

    public async Task Fill(AddOrUpdateFormDto newFormDto, uint templateId, uint authorId)
    {
        if(newFormDto == null)
            throw new InvalidInputDataException("Transmitted form can't be null");
        
        if(!await templateRepository.Exist(templateId))
            throw new NotFoundException("Template");
        
        if(!await userRepository.Exist(authorId))
            throw new NotFoundException("Author");
        
        var template = await templateRepository.GetById(templateId);
        
        if (newFormDto.Answers.Count != template.Questions.Count)
            throw new InvalidInputDataException("Answer count is invalid.");
        
        var newForm = new Form
        {
            TemplateId = templateId,
            AuthorId = authorId,
            CreatedAt = DateTime.UtcNow
        };

        newForm = await repository.Fill(newForm);
        
        for (var i = 0; i < template.Questions.Count; i++)
        {
            var answer = new Answer()
            {
                Value = newFormDto.Answers[i],
                QuestionId = template.Questions[i].Id,
                FormId = newForm.Id
            };
            await answerRepository.Create(answer);
        }
    }

    public async Task Edit(AddOrUpdateFormDto newForm, uint id)
    {
        if (newForm == null)
            throw new InvalidInputDataException("Transmitted form can't be null");
        
        var form = await GetByIdWithoutMapping(id);
        
        if (newForm.Answers.Count != form.Answers.Count)
            throw new InvalidInputDataException("Answer count is invalid.");
        
        for (var i = 0; i < newForm.Answers.Count; i++)
        {
            await answerRepository.Update(newForm.Answers[i], form.Answers[i].Id);
        }
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        var form = await GetByIdWithoutMapping(id);
        
        foreach (var t in form.Answers)
        {
            await answerRepository.Delete(t.Id);
        }
        
        await repository.Delete(id);
    }

    private async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Form");
    }

    public async Task<uint?> GetOwnerId(uint id)
    {
        await Exist(id);
        var form = await repository.GetById(id);
        return form.AuthorId;
    }
}