using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;

namespace Coursework.Application.Services;

public class CommentService(
    ICommentRepository repository,
    ITemplateRepository templateRepository,
    IUserRepository userRepository) : ICommentService
{
    public async Task<List<GetCommentDto>> GetAllByTemplate(uint templateId)
    {
        if(!await templateRepository.Exist(templateId))
            throw new NotFoundException("Template");
        
        var comments = await repository.GetAllByTemplate(templateId);
        
        return comments.Select(CommentMapping.ToGetCommentDto).ToList();
    }

    public async Task<GetCommentDto> GetById(uint id)
    {
        await Exist(id);
        
        return CommentMapping.ToGetCommentDto(await repository.GetById(id));
    }

    public async Task Add(AddCommentDto comment, uint templateId, uint authorId)
    {
        if(comment == null)
            throw new InvalidDataException("Transient comment can't be null");
        
        if (string.IsNullOrWhiteSpace(comment.Content))
            throw new InvalidInputDataException("Comment content can't be empty.");
        
        if(!await templateRepository.Exist(templateId))
            throw new NotFoundException("Template");
        if(!await userRepository.Exist(authorId))
            throw new NotFoundException("User");
            
        var newComment = CommentMapping.FromAddCommentDto(comment);
        newComment.TemplateId = templateId;
        newComment.AuthorId = authorId;
        
        await repository.Add(newComment);
    }

    public async Task Update(UpdateCommentDto newContent, uint id)
    {
        if(newContent.Content == string.Empty)
            throw new InvalidInputDataException("Comment content can't be empty.");

        await Exist(id);
        
        await repository.Update(newContent.Content, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    private async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Comment");
    }
}