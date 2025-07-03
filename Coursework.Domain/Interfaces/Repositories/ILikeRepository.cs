using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ILikeRepository
{
    public Task<Like> GetById(uint id);
    public Task Add(Like like);
    public Task Delete(uint authorId, uint templateId);
    public Task<bool> Exist(uint id);
    public Task<bool> Exist(uint authorId, uint templateId);
}