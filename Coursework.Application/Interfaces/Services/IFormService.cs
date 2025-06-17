using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IFormService
{
    public Task<List<GetFormDto>> GetAllByTemplate(uint templateId);
    public Task<GetFormDto> GetById(uint id);
    public Task Fill(AddOrUpdateFormDto newFormDto, uint templateId, uint authorId);
    public Task Edit(AddOrUpdateFormDto newForm, uint id);
    public Task Delete(uint id);
}