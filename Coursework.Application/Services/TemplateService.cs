using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class TemplateService(
    ITemplateRepository repository,
    IUserRepository userRepository) : ITemplateService
{
    public async Task<List<GetTemplateDto>> GetAll(UserForTemplate user)
    {
        if(!await userRepository.Exist(user.Email))
            throw new NotFoundException("User with email");
        
        var templates = await repository.GetAll(user.Email);

        return templates.Select(TemplateMapping.ToGetTemplateDto).ToList();
    }

    public async Task<GetTemplateDto> GetById(uint id)
    {
        await Exist(id);
        
        return TemplateMapping.ToGetTemplateDto(await repository.GetById(id));
    }

    public async Task Create(AddTemplateDto template, uint authorId)//Todo проверить есть ли такой тэг если нет то добавить, добавить добавление вопросов
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

        if(!await userRepository.Exist(authorId))
            throw new NotFoundException("User");
        
        var newTemplate = TemplateMapping.FromAddTemplateDto(template);
        newTemplate.AuthorId = authorId;
        
        await repository.Create(newTemplate);
    }

    public async Task Update(UpdateTemplateDto template, uint id)//todo убрать возможность повтора везде
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

    public async Task Delete(uint id)//todo удалять сразу вопросы
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task AddAuthorizedUsers(AuthorizedUserDto users, uint id)
    {
        if(users.Emails.Count == 0)
            throw new InvalidInputDataException("Emails cannot be empty");

        await Exist(id);

        var template = await GetByIdForAuthorizedUser(id);
        
        await repository.AddAuthorizedUser(template, users.Emails);
    }

    public async Task DeleteAuthorizedUsers(AuthorizedUserDto users, uint id)
    {
        if(users.Emails.Count == 0)
            throw new InvalidInputDataException("Emails cannot be empty");

        await Exist(id);

        var template = await GetByIdForAuthorizedUser(id);
        
        await repository.DeleteAuthorizedUser(template, users.Emails);
    }

    private async Task Exist(uint id)
    {
        if (!await repository.Exist(id))
            throw new NotFoundException("Template");
    }

    private async Task<bool> Exist(string title) =>
        await repository.Exist(title);
    
    private async Task<Template> GetByIdForAuthorizedUser(uint id) =>
        await repository.GetByIdForAuthorizedUser(id);
}