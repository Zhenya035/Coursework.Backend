using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ITagRepository
{
    public Task Add(Tag tag);
}