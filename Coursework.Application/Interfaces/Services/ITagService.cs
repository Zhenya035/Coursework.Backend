using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ITagService
{
    public Task<List<GetTagDto>> GetAll();
    public Task<GetTagDto> GetById(int id);
    public Task Add(AddTagDto tag);
    public Task Update(string name, uint id);
    public Task Delete(int id);
    public Task Exist(uint id);
}