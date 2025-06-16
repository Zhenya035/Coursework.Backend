using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class TemplateService(
    ITemplateRepository repository,
    IUserService userService) : ITemplateService
{
    public async Task<List<GetTemplateDto>> GetAll(string email)
    {
        var templates = await repository.GetAll(email);

        return templates.Select(TemplateMapping.ToGetTemplateDto).ToList();
    }

    public async Task<GetTemplateDto> GetById(uint id)
    {
        await Exist(id);
        
        return TemplateMapping.ToGetTemplateDto(await repository.GetById(id));
    }

    public async Task Create(AddTemplateDto template, uint authorId)
    {
        if(template == null)
            throw new InvalidInputDataException("Template cannot be null");
        
        if(string.IsNullOrWhiteSpace(template.Title) ||
           string.IsNullOrWhiteSpace(template.Description) ||
           template.Images.Count == 0 ||
           template.Tags.Count == 0)
            throw new InvalidInputDataException("Incorrect template data");

        if (await Exist(template.Title))
            throw new AlreadyAddedException("Template");

        await userService.Exist(authorId);
        
        var newTemplate = TemplateMapping.FromAddTemplateDto(template);
        newTemplate.AuthorId = authorId;
        
        await repository.Create(newTemplate);
    }

    public async Task Update(UpdateTemplateDto template, uint id)
    {
        if(template == null)
            throw new InvalidInputDataException("Template cannot be null");
        
        if(string.IsNullOrWhiteSpace(template.Title) ||
           string.IsNullOrWhiteSpace(template.Description) ||
           template.Images.Count == 0)
            throw new InvalidInputDataException("Incorrect template data");

        await Exist(id);
        
        var newTemplate = TemplateMapping.FromUpdateTemplateDto(template);
        
        await repository.Update(newTemplate, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task AddAuthorizedUser(List<string> emails, uint templateId)
    {
        if(emails.Count == 0)
            throw new InvalidInputDataException("Emails cannot be empty");

        await Exist(templateId);

        var template = await GetByIdForAuthorizedUser(templateId);
        
        await repository.AddAuthorizedUser(template, emails);
    }

    public async Task DeleteAuthorizedUser(List<string> emails, uint templateId)
    {
        if(emails.Count == 0)
            throw new InvalidInputDataException("Emails cannot be empty");

        await Exist(templateId);

        var template = await GetByIdForAuthorizedUser(templateId);
        
        await repository.DeleteAuthorizedUser(template, emails);
    }

    public async Task Exist(uint id)
    {
        if (!await repository.Exist(id))
            throw new NotFoundException("Template");
    }

    private async Task<bool> Exist(string title) =>
        await repository.Exist(title);
    
    private async Task<Template> GetByIdForAuthorizedUser(uint id) =>
        await repository.GetByIdForAuthorizedUser(id);
}