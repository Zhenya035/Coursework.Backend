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
            .Include(a => a.Question)
            .FirstAsync(a => a.Id == id);
        
        return answer;
    }

    public async Task Create(Answer answer)
    {
        await context.Answers.AddAsync(answer);
        await context.SaveChangesAsync();
    }

    public async Task Update(string newValue, uint id) =>
        await context.Answers
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(a => a
                .SetProperty(a => a.Value, newValue)
            );

    public async Task Delete(uint id) => 
        await context.Answers
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

    public async Task<bool> Exist(uint id) =>
        await context.Answers
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id) != null;
}