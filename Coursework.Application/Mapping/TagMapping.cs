using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class TagMapping
{
    public static Tag FromAddTagDto(AddOrUpdateTagDto addOrUpdateTag) =>
        new Tag()
        {
            Name = addOrUpdateTag.Name,
        };

    public static GetTagDto ToGetTagDto(Tag tag) =>
        new GetTagDto()
        {
            Id = tag.Id,
            Name = tag.Name,
        };
}