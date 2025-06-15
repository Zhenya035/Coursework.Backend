using Coursework.Domain.Enums;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Infrastructure.Repositories;

public class UserRepository(CourseworkDbContext context) : IUserRepository
{
    public async Task<User> RegisterUser(User user)
    {
        var users = await GetAllUsers();
        
        if (users.Any(u => u.Email == user.Email))
            throw new AlreadyAddedException("User");
        
        var newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
        return newUser.Entity;
    }

    public async Task<User> LoginUser(string email, string password)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        
        if(user == null)
            throw new NotFoundException("User");

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new InvalidPasswordException();
        
        return user;
    }

    public async Task<List<User>> GetAllUsers() =>
        await context.Users
            .AsNoTracking()
            .ToListAsync();

    public async Task<User> GetById(uint id)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if(user == null)
            throw new NotFoundException("User");
        
        return user;
    }

    public async Task UpdateUser(string name, uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Name, name)
            );

    public async Task DeleteUser(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

    public async Task BlockUser(uint id) =>
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u => u
                .SetProperty(u => u.Status, StatusEnum.Blocked)
            );

    public async Task UnBlockUser(uint id) =>
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
}