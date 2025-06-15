using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IFormService
{
    public Task<List<GetFormDto>> GetAllByTemplate(uint templateId);
    public Task<GetFormDto> GetById(uint id);
    public Task FillForm(AddFormDto form, uint templateId, uint authorId);
    public Task EditForm(List<uint> answers, uint id);
    public Task DeleteForm(uint id);
}