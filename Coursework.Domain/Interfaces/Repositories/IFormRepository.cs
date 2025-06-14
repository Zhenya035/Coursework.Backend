using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IFormRepository
{
    public Task<List<Form>> GetAllByTemplate(uint templateId);
    public Task<Form> GetFormById(uint id);
    public Task FillForm(Form form);
    public Task EditForm(Form form, uint id);
    public Task DeleteForm(uint id);
}