using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITemplateRepository
{
    public Task<List<Template>> GetAll(string email, string role);
    public Task<Template> GetById(uint id);
    public Task<Template> GetByIdForAuthorizedUser(uint id);
    public Task<uint> Create(Template template);
    public Task Update(Template template, uint id);
    public Task Delete(uint id);
    public Task AddAuthorizedUser(Template template, List<string> emails);
    public Task DeleteAuthorizedUser(Template template, List<string> emails);
    public Task<bool> Exist(uint id);
    public Task<bool> Exist(string title);
    public Task<bool> Exist(string title, uint id);
}