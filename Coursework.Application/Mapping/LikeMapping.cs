using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class LikeMapping
{
    public static GetLikeDto ToGetLikeDto(Like like) =>
        new GetLikeDto()
        {
            Id = like.Id,
            TemplateTitle = like.Template.Title,
            Author = like.Author.Name
        };
}