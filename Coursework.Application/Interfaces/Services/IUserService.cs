using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IUserService
{
    public Task<AuthorizationDto> Register(RegisterUserDto user);
    public Task<AuthorizationDto> Login(LoginUserDto user);
    public Task<List<GetUserDto>> GetAll();
    public Task<GetUserDto> GetById(uint id);
    public Task Update(UpdateUserNameDto user, uint id);
    public Task Delete(uint id);
    public Task Block(uint id);
    public Task UnBlock(uint id);
    public Task MakeAdmin(uint id);
    public Task MakeUser(uint id);
}