using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User> Register(User user);
    public Task<User> Login(string email, string password);
    public Task<List<User>> GetAll();
    public Task<User> GetById(uint id);
    public Task Update(string name, uint id);
    public Task Delete(uint id);
    public Task Block(uint id);
    public Task UnBlock(uint id);
    public Task MakeAdmin(uint id);
    public Task MakeUser(uint id);
    public Task Exist(uint id);
}