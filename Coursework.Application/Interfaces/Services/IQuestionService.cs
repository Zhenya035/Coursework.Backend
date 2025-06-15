using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IQuestionService
{
    public Task<List<GetQuestionDto>> GetAllQuestionsByTemplate(uint templateId);
    public Task<GetQuestionDto> GetQuestionById(uint id);
    public Task AddQuestion(AddQuestionDto question, uint templateId);
    public Task UpdateQuestion(UpdateQuestionDto question, uint id);
    public Task DeleteQuestion(uint id);
    public Task MakeDisplay(uint id);
    public Task MakeNotDisplay(uint id);
}