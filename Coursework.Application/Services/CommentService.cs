using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class CommentService(
    ICommentRepository repository,
    ITemplateRepository templateRepository,
    IUserRepository userRepository) : ICommentService, IOwnerService<Comment>
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

    public async Task Delete(List<uint> ids)
    {
        foreach (var id in ids)
            await Exist(id);

        await repository.Delete(ids);
    }

    private async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Comment");
    }

    public async Task<uint?> GetOwnerId(uint id)
    {
        await Exist(id);
        var comment = await repository.GetById(id);
        return comment.AuthorId;
    }
}