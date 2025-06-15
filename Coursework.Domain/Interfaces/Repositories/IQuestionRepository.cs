using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IQuestionRepository
{
    public Task<List<Question>> GetAllByTemplate(uint templateId);
    public Task<Question> GetById(uint id);
    public Task Add(Question question);
    public Task Update(Question question, uint id);
    public Task Delete(uint id);
    public Task MakeDisplay(uint id);
    public Task MakeNotDisplay(uint id);
    public Task Exist(uint id);
}