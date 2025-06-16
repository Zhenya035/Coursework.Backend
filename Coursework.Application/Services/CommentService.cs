using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;

namespace Coursework.Application.Services;

public class CommentService(
    ICommentRepository repository,
    ITemplateService templateService,
    IUserService userService) : ICommentService
{
    public async Task<List<GetCommentDto>> GetAllByTemplate(uint templateId)
    {
        await templateService.Exist(templateId);
        
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
        
        if (comment.Content != string.Empty)
            throw new InvalidInputDataException("Comment content can't be empty.");
        
        await templateService.Exist(templateId);
        await userService.Exist(authorId);
        
        var newComment = CommentMapping.FromAddCommentDto(comment);
        newComment.TemplateId = templateId;
        newComment.AuthorId = authorId;
        
        await repository.Add(newComment);
    }

    public async Task Update(string content, uint id)
    {
        if(content == string.Empty)
            throw new InvalidInputDataException("Comment content can't be empty.");

        await Exist(id);
        
        await repository.Update(content, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Comment");
    }
}