using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITagRepository
{
    public Task<List<Tag>> GetAll();
    public Task<Tag> GetById(int id);
    public Task Add(Tag tag);
    public Task Update(string name, uint id);
    public Task Delete(int id);
    public Task Exist(uint id);
}