using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IAnswerService
{
    public Task<List<GetAnswerDto>> GetAllByForm(uint formId);
    public Task<GetAnswerDto> GetById(uint id);
}