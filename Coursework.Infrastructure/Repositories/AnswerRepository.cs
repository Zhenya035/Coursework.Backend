using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class AnswerRepository(CourseworkDbContext context) : IAnswerRepository
{
    public async Task<List<Answer>> GetAllByForm(uint formId) => 
        await context.Answers
            .AsNoTracking()
            .Include(f => f.Question)
            .Where(a => a.FormId == formId)
            .OrderBy(a => a.QuestionId)
            .ToListAsync();

    public async Task<Answer> GetById(uint id)
    {
        var answer = await context.Answers
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
        
        if(answer == null)
            throw new NotFoundException("answer");
        
        return answer;
    }

    public async Task CreateAnswer(Answer answer)
    {
        await context.Answers.AddAsync(answer);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAnswer(string newValue, uint id) =>
        await context.Answers
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.Value, newValue)
            );

    public async Task DeleteAnswer(uint id) => 
        await context.Answers
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
}