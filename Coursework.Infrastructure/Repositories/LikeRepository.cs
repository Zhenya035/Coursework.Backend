using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class LikeRepository(CourseworkDbContext context) : ILikeRepository
{
    public async Task<Like> GetById(uint id)
    {
        var like = await context.Likes
            .AsNoTracking()
            .Include(l => l.Template)
            .Include(l => l.Author)
            .FirstAsync(l => l.Id == id);

        return like;
    }
    
    public async Task Add(Like like)
    {
        await context.Likes.AddAsync(like);
        await context.SaveChangesAsync();
    }

    public async Task Delete(uint id) =>
        await context.Likes
            .Where(l => l.Id == id)
            .ExecuteDeleteAsync();

    public async Task<bool> Exist(uint id) =>
        await context.Likes
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id) != null;

    public async Task<bool> Exist(uint authorId, uint templateId) =>
        await context.Likes
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.AuthorId == authorId && l.TemplateId == templateId) != null;
}