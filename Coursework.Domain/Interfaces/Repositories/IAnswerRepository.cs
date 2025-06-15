using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IAnswerRepository
{
    public Task<List<Answer>> GetAllByForm(uint formId);
    public Task<Answer> GetById(uint id);
    public Task CreateAnswer(Answer answer);
    public Task UpdateAnswer(string newValue, uint id);
    public Task DeleteAnswer(uint id);
}