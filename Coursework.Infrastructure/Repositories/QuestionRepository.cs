using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class QuestionRepository(CourseworkDbContext context) : IQuestionRepository
{
    public async Task<List<Question>> GetAllByTemplate(uint templateId) => 
        await context.Questions
            .AsNoTracking()
            .Where(q => q.TemplateId == templateId)
            .ToListAsync();
    
    public async Task<Question> GetById(uint id)
    {
        var question = await context.Questions
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
        
        if(question == null)
            throw new NotFoundException("Question");
        
        return question;
    }

    public async Task Add(Question question)
    {
        await context.Questions.AddAsync(question);
        await context.SaveChangesAsync();
    }

    public async Task Update(Question newQuestion, uint id) =>
        await context.Questions
            .Where(q => q.Id == id)
            .ExecuteUpdateAsync(q => q
                .SetProperty(q => q.Name, newQuestion.Name)
                .SetProperty(q => q.Type, newQuestion.Type)
                .SetProperty(q => q.Description, newQuestion.Description)
            );

    public async Task Delete(uint id) =>
        await context.Questions
            .Where(q => q.Id == id)
            .ExecuteDeleteAsync();

    public async Task MakeDisplay(uint id) =>
        await context.Questions
            .Where(q => q.Id == id)
            .ExecuteUpdateAsync(q => q
                .SetProperty(q => q.IsDisplayed, true)
            );

    public async Task MakeNotDisplay(uint id) =>
        await context.Questions
            .Where(q => q.Id == id)
            .ExecuteUpdateAsync(q => q
                .SetProperty(q => q.IsDisplayed, false)
            );

    public async Task Exist(uint id)
    {
        if (await context.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id) == null) 
            throw new NotFoundException("Question");
    }
}