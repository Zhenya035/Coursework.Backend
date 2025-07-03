using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ICommentService
{
    public Task<List<GetCommentDto>> GetAllByTemplate(uint templateId);
    public Task<GetCommentDto> GetById(uint id);
    public Task Add(AddCommentDto comment, uint templateId, uint authorId);
    public Task Update(UpdateCommentDto newContent, uint id);
    public Task Delete(List<uint> ids);
}