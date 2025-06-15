using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IQuestionService
{
    public Task<List<GetQuestionDto>> GetAllByTemplate(uint templateId);
    public Task<GetQuestionDto> GetById(uint id);
    public Task Add(AddQuestionDto question, uint templateId);
    public Task Update(UpdateQuestionDto question, uint id);
    public Task Delete(uint id);
    public Task MakeDisplay(uint id);
    public Task MakeNotDisplay(uint id);
    public Task Exist(uint id);
}