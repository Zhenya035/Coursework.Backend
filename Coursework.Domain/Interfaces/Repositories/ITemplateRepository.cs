using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITemplateRepository
{
    public Task<List<Template>> GetAllTemplates();
    public Task<Template> GetById(uint id);
    public Task CreateTemplate(Template template);
    public Task UpdateTemplate(Template template, uint id);
    public Task DeleteTemplate(uint id);
    public Task AddAuthorizedUser(Template template, List<string> emails);
    public Task DeleteAuthorizedUser(Template template, List<string> emails);
}