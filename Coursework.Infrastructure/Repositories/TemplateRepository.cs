using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class TemplateRepository(CourseworkDbContext context) : ITemplateRepository
{
    public async Task<List<Template>> GetAll() =>
        await context.Templates.AsNoTracking().ToListAsync();

    public async Task<Template> GetById(uint id)
    {
        var template = await context.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
        
        if(template == null)
            throw new NotFoundException("Template");
        
        return template;
    }

    public async Task Create(Template template)
    {
        await context.Templates.AddAsync(template);
        await context.SaveChangesAsync();
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

    public async Task Exist(uint id)
    {
        if (await context.Templates
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id) == null) 
            throw new NotFoundException("Template");
    }
}