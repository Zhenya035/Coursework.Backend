using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class CommentMapping
{
    public static Comment FromAddCommentDto(AddCommentDto addCommentDto) =>
        new Comment()
        {
            Content = addCommentDto.Content
        };

    public static GetCommentDto ToGetCommentDto(Comment comment) =>
        new GetCommentDto()
        {
            Id = comment.Id,
            Content = comment.Content,
            Author = comment.Author.Name,
            CreatedAt = comment.CreatedAt,
        };
}