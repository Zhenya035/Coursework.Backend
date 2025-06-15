using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IAnswerService
{
    public Task<List<GetAnswerDto>> GetAllByForm(uint formId);
    public Task<GetAnswerDto> GetById(uint id);
    public Task CreateAnswer(AddAnswerDto answer, uint formId, uint questionId);
    public Task UpdateAnswer(string newValue, uint id);
    public Task DeleteAnswer(uint id);
}