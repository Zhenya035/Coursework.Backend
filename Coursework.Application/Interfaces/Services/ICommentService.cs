using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ICommentService
{
    public Task<List<GetCommentDto>> GetAllByTemplate(uint templateId);
    public Task<GetCommentDto> GetById(uint id);
    public Task Add(AddCommentDto comment, uint templateId, uint authorId);
    public Task Update(string content, uint id);
    public Task Delete(uint id);
    public Task Exist(uint id);
}