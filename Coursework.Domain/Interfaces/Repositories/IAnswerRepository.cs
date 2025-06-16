using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IAnswerRepository
{
    public Task<List<Answer>> GetAllByForm(uint formId);
    public Task<Answer> GetById(uint id);
    public Task Create(Answer answer);
    public Task Update(string newValue, uint id);
    public Task Delete(uint id);
    public Task<bool> Exist(uint id);
}