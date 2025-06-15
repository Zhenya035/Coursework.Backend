using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IQuestionRepository
{
    public Task<List<Question>> GetAllQuestionsByTemplate(uint templateId);
    public Task<Question> GetQuestionById(uint id);
    public Task AddQuestion(Question question);
    public Task UpdateQuestion(Question question, uint id);
    public Task DeleteQuestion(uint id);
    public Task MakeDisplay(uint id);
    public Task MakeNotDisplay(uint id);
}