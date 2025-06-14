using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IAnswerRepository
{
    public Task<List<Answer>> GetAllByForm(uint formId);
    public Task CreateAnswer(Answer answer);
    public Task UpdateAnswer(Answer answer, uint id);
    public Task DeleteAnswer(uint id);
}