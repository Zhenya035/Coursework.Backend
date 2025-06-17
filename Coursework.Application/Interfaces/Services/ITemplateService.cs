using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface ITemplateService
{
    public Task<List<GetTemplateDto>> GetAll(UserForTemplate user);
    public Task<GetTemplateDto> GetById(uint id);
    public Task Create(AddTemplateDto template, uint authorId);
    public Task Update(UpdateTemplateDto template, uint id);
    public Task Delete(uint id);
    public Task AddAuthorizedUsers(AuthorizedUserDto users, uint id);
    public Task DeleteAuthorizedUsers(AuthorizedUserDto users, uint id);
}