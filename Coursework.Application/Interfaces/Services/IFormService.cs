using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IFormService
{
    public Task<List<GetFormDto>> GetAllByTemplate(uint templateId);
    public Task<GetFormDto> GetById(uint id);
    public Task Fill(AddFormDto form, uint templateId, uint authorId);
    public Task Edit(List<uint> answerIds, uint id);
    public Task Delete(uint id);
    public Task Exist(uint id);
}