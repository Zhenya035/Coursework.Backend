using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITemplateRepository
{
    public Task<List<Template>> GetAll(string email);
    public Task<Template> GetById(uint id);
    public Task<Template> GetByIdForAuthorizedUser(uint id);
    public Task Create(Template template);
    public Task Update(Template template, uint id);
    public Task Delete(uint id);
    public Task AddAuthorizedUser(Template template, List<string> emails);
    public Task DeleteAuthorizedUser(Template template, List<string> emails);
    public Task<bool> Exist(uint id);
    public Task<bool> Exist(string Title);
}