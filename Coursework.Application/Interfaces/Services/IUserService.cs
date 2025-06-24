using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IUserService
{
    public Task<AuthorizationDto> Register(RegisterUserDto user);
    public Task<AuthorizationDto> Login(LoginUserDto user);
    public Task<List<GetUserDto>> GetAll(uint id);
    public Task<GetUserDto> GetById(uint id);
    public Task Update(UpdateUserNameDto user, uint id);
    public Task Delete(List<uint> ids);
    public Task Block(List<uint> ids);
    public Task UnBlock(List<uint> ids);
    public Task MakeAdmin(List<uint> ids);
    public Task MakeUser(List<uint> ids);
}