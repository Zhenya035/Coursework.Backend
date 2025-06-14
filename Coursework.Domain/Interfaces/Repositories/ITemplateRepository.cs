using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITemplateRepository
{
    public Task<List<Template>> GetAllTemplates();
    public Task<Template> GetTemplateById(uint id);
    public Task CreateTemplate(Template template);
    public Task UpdateTemplate(Template template, uint id);
    public Task DeleteTemplate(uint id);
    public Task AddAuthorizedUser(List<string> email, uint id);
    public Task DeleteAuthorizedUser(List<string> email, uint id);
}