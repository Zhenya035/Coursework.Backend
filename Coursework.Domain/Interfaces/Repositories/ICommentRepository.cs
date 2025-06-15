using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<List<Comment>> GetAllByTemplate(uint templateId);
    public Task<Comment> GetById(uint id);
    public Task Add(Comment comment);
    public Task Update(string content, uint id);
    public Task Delete(uint id);
}