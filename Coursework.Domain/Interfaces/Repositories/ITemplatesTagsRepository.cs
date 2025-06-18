namespace Coursework.Domain.Interfaces.Repositories;

public interface ITemplatesTagsRepository
{
    public Task Add(uint templateId, List<uint> tagIds);
}