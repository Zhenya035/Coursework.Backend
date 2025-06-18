using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITemplatesTagsRepository
{
    public Task<List<TemplatesTags>> GetAllByTemplate(uint templateId);
    public Task Add(uint templateId, List<uint> tagIds);
    public Task Delete(uint templateId, uint tagId);
}