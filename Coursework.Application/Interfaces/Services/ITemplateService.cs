using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ITemplateService
{
    public Task<List<GetTemplateDto>> GetAll(string email);
    public Task<GetTemplateDto> GetById(uint id);
    public Task Create(AddTemplateDto template, uint authorId);
    public Task Update(UpdateTemplateDto template, uint id);
    public Task Delete(uint id);
    public Task AddAuthorizedUser(List<string> emails, uint templateId);
    public Task DeleteAuthorizedUser(List<string> emails, uint templateId);
    public Task Exist(uint id);
}