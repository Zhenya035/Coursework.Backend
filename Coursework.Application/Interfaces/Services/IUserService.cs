using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;

namespace Coursework.Application.Interfaces.Services;

public interface IUserService
{
    public Task<AuthorizationDto> RegisterUser(RegisterUserDto user);
    public Task<AuthorizationDto> LoginUser(LoginUserDto user);
    public Task<List<GetUserDto>> GetAllUsers();
    public Task<GetUserDto> GetById(uint id);
    public Task UpdateUser(string name, uint id);
    public Task DeleteUser(uint id);
    public Task BlockUser(uint id);
    public Task UnBlockUser(uint id);
    public Task MakeAdmin(uint id);
    public Task MakeUser(uint id);
}