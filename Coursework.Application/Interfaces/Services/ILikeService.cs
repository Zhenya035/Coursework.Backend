using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ILikeService
{
    public Task<GetLikeDto> GetById(uint id);
    public Task Add(uint templateId, uint authorId);
    public Task Delete(uint templateId, uint authorId);
    public Task<bool> Exist(uint authorId, uint templateId);
}