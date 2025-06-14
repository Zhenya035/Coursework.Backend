using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User> RegisterUser(User user);
    public Task<User> LoginUser(string email, string password);
    public Task<List<User>> GetAllUsers();
    public Task<User> GetUserById(uint id);
    public Task UpdateUser(User user, uint id);
    public Task DeleteUser(uint id);
    public Task BlockUser(uint id);
    public Task UnBlockUser(uint id);
    public Task MakeAdmin(uint id);
    public Task MakeUser(uint id);
}