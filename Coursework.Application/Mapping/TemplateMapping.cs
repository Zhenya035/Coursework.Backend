using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class TemplateMapping
{
    public static Template FromAddTemplateDto(AddTemplateDto addTemplateDto) =>
        new Template()
        {
            Title = addTemplateDto.Title,
            Description = addTemplateDto.Description,
            Images = addTemplateDto.Images,
            AuthorisedEmails = addTemplateDto.AuthorisedEmails
        };
    
    public static Template FromUpdateTemplateDto(UpdateTemplateDto updateTemplateDto) =>
        new Template()
        {
            Title = updateTemplateDto.Title,
            Description = updateTemplateDto.Description,
            Images = updateTemplateDto.Images,
            UpdatedAt = updateTemplateDto.UpdatedAt
        };

    public static GetTemplateDto ToGetTemplateDto(Template template) =>
        new GetTemplateDto()
        {
            Id = template.Id,
            Title = template.Title,
            Description = template.Description,
            Images = template.Images,
            AuthorisedEmails = template.AuthorisedEmails,
            CreatedAt = template.CreatedAt,
            UpdatedAt = template.UpdatedAt,
            Tags = template.Tags.Select(t => t.Tag.Name).ToList(),
            LikesCount = template.Likes.Count,
            QuestionsCount = template.Questions.Count,
            Comments = template.Comments.Select(CommentMapping.ToGetCommentDto).ToList(),
            FormsCount = template.Forms.Count,
            Author = template.Author.Name
        };
}