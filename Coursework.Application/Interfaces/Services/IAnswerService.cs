using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Interfaces.Services;

public interface IAnswerService
{
    public Task<List<GetAnswerDto>> GetAllByForm(uint formId);
    public Task<GetAnswerDto> GetById(uint id);
    public Task<Answer> GetByIdWithoutMapping(uint id);
    public Task Create(AddAnswerDto answer, uint questionId);
    public Task Update(UpdateAnswerDto newValue, uint id);
    public Task Delete(uint id);
}