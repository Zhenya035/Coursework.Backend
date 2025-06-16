using Coursework.Domain.Enums;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class UserRepository(CourseworkDbContext context) : IUserRepository
{
    public async Task<User> Register(User user)
    {
        var newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return newUser.Entity;
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        
        return user;
    }

    public async Task<List<User>> GetAll() =>
        await context.Users
            .AsNoTracking()
            .ToListAsync();

    public async Task<User> GetById(uint id)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        
        return user;
    }

    public async Task Update(string name, uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Name, name)
            );

    public async Task Delete(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

    public async Task Block(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Status, StatusEnum.Blocked)
            );

    public async Task UnBlock(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Status, StatusEnum.Active)
            );

    public async Task MakeAdmin(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Role, RoleEnum.Admin)
            );

    public async Task MakeUser(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Role, RoleEnum.User)
            );

    public async Task<bool> Exist(uint id) =>
        await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id) != null;
    
    public async Task<bool> Exist(string email) =>
        await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email) != null;
}