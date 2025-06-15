using Coursework.Domain.Exceptions;
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
            .Include(f => f.Answers)
                .ThenInclude(a => a.Question)
            .ToListAsync();

    public async Task<Form> GetById(uint id)
    {
        var form = await context.Forms
            .AsNoTracking()
            .Include(f => f.Answers)
                .ThenInclude(a => a.Question)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (form == null)
            throw new NotFoundException("form");

        return form;
    }

    public async Task FillForm(Form form)
    {
        await context.Forms.AddAsync(form);
        await context.SaveChangesAsync();
    }

    public async Task EditForm(Form form, List<Answer> answers)
    {
        form.Answers.Clear();
        form.Answers.AddRange(answers);
        
        await context.SaveChangesAsync();
    }

    public async Task DeleteForm(uint id)
    {
        var form = await context.Forms
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
        
        if(form == null)
            throw new NotFoundException("form");
        
        await context.Forms
            .Where(f => f.Id == id)
            .ExecuteDeleteAsync();
    }
}