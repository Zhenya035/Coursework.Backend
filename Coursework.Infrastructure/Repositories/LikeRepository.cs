using Coursework.Domain.Exceptions;
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
            .FirstOrDefaultAsync(l => l.Id == id);

        if (like == null)
            throw new NotFoundException("Like");

        return like;
    }
    
    public async Task Add(Like like)
    {
        await context.Likes.AddAsync(like);
        await context.SaveChangesAsync();
    }

    public async Task Delete(uint id)
    {
        await context.Likes
            .Where(l => l.Id == id)
            .ExecuteDeleteAsync();
    }
}