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
    IFormRepository formRepository,
    ITagRepository tagRepository,
    ITemplatesTagsRepository templateTagsRepository,
    IQuestionRepository questionRepository,
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

    public async Task Create(AddTemplateDto template, uint authorId)
    {
        if(template == null)
            throw new InvalidInputDataException("Template cannot be null");
        
        if(string.IsNullOrWhiteSpace(template.Title) ||
           string.IsNullOrWhiteSpace(template.Description) ||
           template.Images.Count == 0 ||
           template.Tags.Count == 0 ||
           template.Questions.Count == 0)
            throw new InvalidInputDataException("Incorrect template data");

        if (await Exist(template.Title))
            throw new AlreadyAddedException("Template");

        if(!await userRepository.Exist(authorId))
            throw new NotFoundException("User");
        
        var tags = await tagRepository.GetAll();
        var tagIds = new List<uint>();
        foreach (var tag in template.Tags.Where(tag => !tags.Select(t => t.Name).Contains(tag)))
            tagIds.Add(await tagRepository.Add(new Tag { Name = tag }));
        
        var newTemplate = TemplateMapping.FromAddTemplateDto(template);
        newTemplate.AuthorId = authorId;
        
        var templateId = await repository.Create(newTemplate);
        
        await templateTagsRepository.Add(templateId, tagIds);
        
        foreach (var question in template.Questions.Select(QuestionMapping.FromAddQuestionDto))
        {
            question.TemplateId = templateId;
            
            await questionRepository.Add(question);
        }
    }

    public async Task Update(UpdateTemplateDto updateTemplateDto, uint id)
    {
        if(updateTemplateDto == null)
            throw new InvalidInputDataException("Template cannot be null");
        
        if(string.IsNullOrWhiteSpace(updateTemplateDto.Title) ||
           string.IsNullOrWhiteSpace(updateTemplateDto.Description) ||
           updateTemplateDto.Images.Count == 0)
            throw new InvalidInputDataException("Incorrect template data");
        
        if(await Exist(updateTemplateDto.Title))
            throw new AlreadyAddedException("Template with this title");

        var template = await repository.GetById(id);
        
        if(updateTemplateDto.Questions.Count != template.Questions.Count)
            throw new InvalidInputDataException("Incorrect question count");
        
        var questions = updateTemplateDto.Questions.Select(QuestionMapping.FromUpdateQuestionDto).ToList();
        for (var i = 0; i < questions.Count; i++)
            await questionRepository.Update(questions[i], template.Questions[i].Id);

        var existingTags = template.Tags.Select(tt => tt.Tag).ToList();
        
        var tagForDelete = existingTags.Where(t => !updateTemplateDto.Tags.Contains(t.Name)).ToList();
        foreach (var tFD in tagForDelete)
            await templateTagsRepository.Delete(template.Id, tFD.Id);
        
        var tagForAdd = updateTemplateDto.Tags.Where(t => !existingTags
                .Select(eT => eT.Name).Contains(t)).ToList();
        
        var newTagsId = new List<uint>();
        foreach (var tag in tagForAdd)
        {
            if(!await tagRepository.Exist(tag))
            {
                var newTag = new Tag { Name = tag };
                newTagsId.Add(await tagRepository.Add(newTag));
            }
            else
            {
                var newTag = await tagRepository.GetByName(tag);
                newTagsId.Add(newTag.Id);
            }
        }
        await templateTagsRepository.Add(template.Id, newTagsId);
        
        var newTemplate = TemplateMapping.FromUpdateTemplateDto(updateTemplateDto);
        
        await repository.Update(newTemplate, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        var template = await repository.GetById(id);
        
        foreach (var templateForm in template.Forms)
        {
            await formRepository.Delete(templateForm.Id);
        }
        
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