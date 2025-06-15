using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ILikeService
{
    public Task<GetLikeDto> GetById(uint id);
    public Task Add(uint authorId, uint templateId);
    public Task Delete(uint id);
}