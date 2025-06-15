using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IFormRepository
{
    public Task<List<Form>> GetAllByTemplate(uint templateId);
    public Task<Form> GetById(uint id);
    public Task FillForm(Form form);
    public Task EditForm(Form form, List<Answer> answers);
    public Task DeleteForm(uint id);
}