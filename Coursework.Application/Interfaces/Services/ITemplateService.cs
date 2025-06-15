using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ITemplateService
{
    public Task<List<GetTemplateDto>> GetAllTemplates();
    public Task<GetTemplateDto> GetById(uint id);
    public Task CreateTemplate(AddTemplateDto template, uint authorId);
    public Task UpdateTemplate(UpdateTemplateDto template, uint id);
    public Task DeleteTemplate(uint id);
    public Task AddAuthorizedUser(List<string> emails, uint templateId);
    public Task DeleteAuthorizedUser(List<string> emails, uint templateId);
}