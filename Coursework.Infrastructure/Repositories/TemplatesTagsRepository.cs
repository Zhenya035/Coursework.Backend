using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Infrastructure.Repositories;

public class TemplatesTagsRepository(CourseworkDbContext context) : ITemplatesTagsRepository
{
    public async Task Add(uint templateId, List<uint> tagIds)
    {
        var templateTags = tagIds.Select(tagId => 
            new TemplatesTags
            {
                TagId = tagId, 
                TemplateId = templateId
            }).ToList();
        
        await context.TemplatesTags.AddRangeAsync(templateTags);
        await context.SaveChangesAsync();
    }
}