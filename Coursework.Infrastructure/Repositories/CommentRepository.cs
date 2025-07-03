using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class CommentRepository(CourseworkDbContext context) : ICommentRepository
{
    public async Task<List<Comment>> GetAllByTemplate(uint templateId) =>
        await context.Comments
            .AsNoTracking()
            .Include(c => c.Author)
            .Where(c => c.TemplateId == templateId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

    public async Task<Comment> GetById(uint id)
    {
        var comment = await context.Comments
            .AsNoTracking()
            .Include(c => c.Author)
            .FirstAsync(c => c.Id == id);
        
        return comment;
    }

    public async Task Add(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
    }

    public async Task Update(string content, uint id) =>
        await context.Comments
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.Content, content)
            );

    public async Task Delete(List<uint> ids) => 
        await context.Comments
            .Where(c => ids.Contains(c.Id))
            .ExecuteDeleteAsync();

    public async Task<bool> Exist(uint id) =>
        await context.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id) != null;
}