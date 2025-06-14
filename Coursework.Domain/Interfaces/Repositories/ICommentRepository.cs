using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<List<Answer>> GetAllByTemplate(uint templateId);
    public Task Add(Comment comment);
    public Task Update(Comment comment, uint id);
    public Task Delete(Comment comment);
}