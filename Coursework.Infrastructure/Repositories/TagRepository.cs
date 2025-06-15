using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class TagRepository(CourseworkDbContext context) : ITagRepository
{
    public async Task<List<Tag>> GetAllTags() =>
        await context.Tags.AsNoTracking().ToListAsync();

    public async Task<Tag> GetTagById(int id)
    {
        var tag = await context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null)
            throw new NotFoundException("Tag");
        
        return tag;
    }
        
    public async Task Add(Tag tag)
    {
        await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync();
    }

    public async Task Update(string name, uint id) =>
        await context.Tags
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(t => t
                .SetProperty(t => t.Name, name)
            );

    public async Task Delete(int id) =>
        await context.Tags
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
}