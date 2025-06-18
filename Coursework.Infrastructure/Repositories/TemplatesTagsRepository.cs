using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class TemplatesTagsRepository(CourseworkDbContext context) : ITemplatesTagsRepository
{
    public async Task<List<TemplatesTags>> GetAllByTemplate(uint templateId) =>
        await context.TemplatesTags
            .AsNoTracking()
            .Where(tt => tt.TemplateId == templateId)
            .Include(tt => tt.Tag)
            .ToListAsync();
    
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

    public async Task Delete(uint templateId, uint tagId)
    {
        await context.TemplatesTags
            .Where(tt => tt.TemplateId == templateId && tt.TagId == tagId)
            .ExecuteDeleteAsync();
    }
}