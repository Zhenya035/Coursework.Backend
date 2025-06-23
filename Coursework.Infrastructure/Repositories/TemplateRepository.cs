using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class TemplateRepository(CourseworkDbContext context) : ITemplateRepository
{
    public async Task<List<Template>> GetAll(string email) =>
        await context.Templates
            .AsNoTracking()
            .Where(t => t.AuthorisedEmails.Count == 0 || t.AuthorisedEmails.Contains(email))
            .Include(t => t.Tags)
            .Include(t => t.Likes)
            .Include(t => t.Comments)
                .ThenInclude(c => c.Author)
            .Include(t => t.Forms)
            .Include(t => t.Author)
            .Include(t => t.Questions)
            .ToListAsync();

    public async Task<Template> GetById(uint id)
    {
        var template = await context.Templates
            .AsNoTracking()
            .Include(t => t.Tags)
                .ThenInclude(t => t.Tag)
            .Include(t => t.Likes)
            .Include(t => t.Comments)
            .Include(t => t.Forms)
            .Include(t => t.Author)
            .Include(t => t.Questions)
            .FirstAsync(t => t.Id == id);
        
        return template;
    }

    public async Task<Template> GetByIdForAuthorizedUser(uint id)
    {
        var template = await context.Templates
            .FirstOrDefaultAsync(t => t.Id == id);
        
        if(template == null)
            throw new NotFoundException("Template");
        
        return template;
    }

    public async Task<uint> Create(Template template)
    {
        var newTemplate = await context.Templates.AddAsync(template);
        await context.SaveChangesAsync();
        
        return newTemplate.Entity.Id;
    }

    public async Task Update(Template template, uint id) =>
        await context.Templates
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(t => t
                .SetProperty(t => t.Title, template.Title)
                .SetProperty(t => t.Description, template.Description)
                .SetProperty(t => t.Images, template.Images)
                .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
            );

    public async Task Delete(uint id) =>
        await context.Templates
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();

    public async Task AddAuthorizedUser(Template template, List<string> emails)
    {
        var newEmails = emails
            .Where(email => !template.AuthorisedEmails.Contains(email))
            .ToList();
        
        template.AuthorisedEmails.AddRange(newEmails);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAuthorizedUser(Template template, List<string> emails)
    {
        template.AuthorisedEmails.RemoveAll(emails.Contains);
        await context.SaveChangesAsync();
    }

    public async Task<bool> Exist(uint id) => 
        await context.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id) != null;
    
    public async Task<bool> Exist(string title) => 
        await context.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Title == title) != null;
}