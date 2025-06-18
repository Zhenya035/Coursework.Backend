using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class TagRepository(CourseworkDbContext context) : ITagRepository
{
    public async Task<List<Tag>> GetAll() =>
        await context.Tags.AsNoTracking().ToListAsync();

    public async Task<Tag> GetById(uint id)
    {
        var tag = await context.Tags
            .AsNoTracking()
            .FirstAsync(t => t.Id == id);
        
        return tag;
    }
        
    public async Task<uint> Add(Tag tag)
    {
        var newTag = await context.Tags.AddAsync(tag);
        await context.SaveChangesAsync();
        
        return newTag.Entity.Id;
    }

    public async Task Update(string name, uint id) =>
        await context.Tags
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(t => t
                .SetProperty(t => t.Name, name)
            );

    public async Task Delete(uint id) =>
        await context.Tags
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();

    public async Task<bool> Exist(uint id) =>
        await context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id) != null;

    public async Task<bool> Exist(string name) =>
        await context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Name == name) != null;

}