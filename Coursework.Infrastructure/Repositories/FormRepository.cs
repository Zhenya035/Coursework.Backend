using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class FormRepository(CourseworkDbContext context) : IFormRepository
{
    public async Task<List<Form>> GetAllByTemplate(uint templateId) =>
        await context.Forms
            .AsNoTracking()
            .Where(f => f.TemplateId == templateId)
            .Include(f => f.Template)
            .Include(f => f.Author)
            .Include(f => f.Answers)
                .ThenInclude(a => a.Question)
            .ToListAsync();

    public async Task<Form> GetById(uint id)
    {
        var form = await context.Forms
            .AsNoTracking()
            .Include(f => f.Template)
            .Include(f => f.Author)
            .Include(f => f.Answers)
                .ThenInclude(a => a.Question)
            .FirstAsync(f => f.Id == id);

        return form;
    }

    public async Task<Form> Fill(Form form)
    {
        var newForm = await context.Forms.AddAsync(form);
        await context.SaveChangesAsync();
        
        return newForm.Entity;
    }

    public async Task Delete(uint id)
    {
        await context.Forms
            .Where(f => f.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> Exist(uint id) =>
        await context.Forms
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id) != null;
}