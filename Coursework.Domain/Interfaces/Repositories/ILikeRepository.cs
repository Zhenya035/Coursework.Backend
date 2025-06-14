using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ILikeRepository
{
    public Task Add(Like like);
    public Task Delete(uint id);
}