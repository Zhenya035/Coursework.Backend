using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITagRepository
{
    public Task<List<Tag>> GetAll();
    public Task<Tag> GetById(uint id);
    public Task Add(Tag tag);
    public Task Update(string name, uint id);
    public Task Delete(uint id);
    public Task<bool> Exist(uint id);
    public Task<bool> Exist(string name);
}