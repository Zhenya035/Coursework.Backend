using Coursework.Domain.Models;

namespace Coursework.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User> Register(User user);
    public Task<User?> GetByEmail(string email);
    public Task<List<User>> GetAll(uint id);
    public Task<User> GetById(uint id);
    public Task Update(string name, uint id);
    public Task Delete(List<uint> ids);
    public Task Block(List<uint> ids);
    public Task UnBlock(List<uint> ids);
    public Task MakeAdmin(List<uint> ids);
    public Task MakeUser(List<uint> ids);
    public Task<bool> Exist(uint id);
    
    public Task<bool> Exist(string email);
}