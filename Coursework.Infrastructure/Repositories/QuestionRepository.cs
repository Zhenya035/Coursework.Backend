using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class QuestionRepository(CourseworkDbContext context) : IQuestionRepository
{
    public async Task<List<Question>> GetAllQuestionsByTemplate(uint templateId) => 
        await context.Questions
            .AsNoTracking()
            .Where(q => q.TemplateId == templateId)
            .ToListAsync();
    
    public async Task<Question> GetQuestionById(uint id)
    {
        var question = await context.Questions
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
        
        if(question == null)
            throw new NotFoundException("Question");
        
        return question;
    }

    public async Task AddQuestion(Question question)
    {
        await context.Questions.AddAsync(question);
        await context.SaveChangesAsync();
    }

    public async Task UpdateQuestion(Question newQuestion, uint id) =>
        await context.Questions
            .Where(q => q.Id == id)
            .ExecuteUpdateAsync(q => q
                .SetProperty(q => q.Name, newQuestion.Name)
                .SetProperty(q => q.Type, newQuestion.Type)
                .SetProperty(q => q.Description, newQuestion.Description)
            );

    public async Task DeleteQuestion(uint id) =>
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
}