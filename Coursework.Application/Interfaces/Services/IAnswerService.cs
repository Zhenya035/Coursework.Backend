using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Interfaces.Services;

public interface IAnswerService
{
    public Task<List<GetAnswerDto>> GetAllByForm(uint formId);
    public Task<GetAnswerDto> GetById(uint id);
    public Task<Answer> GetByIdWithoutMapping(uint id);
    public Task Create(AddAnswerDto answer, uint formId, uint questionId);
    public Task Update(string newValue, uint id);
    public Task Delete(uint id);
    public Task Exist(uint id);
}