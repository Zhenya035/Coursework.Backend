using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IFormRepository
{
    public Task<List<Form>> GetAllByTemplate(uint templateId);
    public Task<Form> GetById(uint id);
    public Task<Form> Fill(Form form);
    public Task Edit(Form form, List<Answer> answers);
    public Task Delete(uint id);
    public Task<bool> Exist(uint id);
}