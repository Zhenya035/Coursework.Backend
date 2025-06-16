using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;

namespace Coursework.Application.Services;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<AuthorizationDto> Register(RegisterUserDto user)
    {
        if(user == null)
            throw new InvalidInputDataException("User can't be null");
        
        if(string.IsNullOrWhiteSpace(user.Email) ||
           string.IsNullOrWhiteSpace(user.Name) ||
           string.IsNullOrWhiteSpace(user.Password))
            throw new InvalidInputDataException("Email, password and name can't be empty");

        await Exist(user.Email);
        
        var newUser = UserMapping.FromRegistrationDto(user);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        
        newUser = await repository.Register(newUser);

        var response = new AuthorizationDto()
        {
            Id = newUser.Id,
            Token = "",
        };
        
        return response;
    }
    
    public async Task<AuthorizationDto> Login(LoginUserDto loginUser)
    {
        if(loginUser == null)
            throw new InvalidInputDataException("User can't be null");
        
        if(string.IsNullOrWhiteSpace(loginUser.Email) ||
           string.IsNullOrWhiteSpace(loginUser.Password))
            throw new InvalidInputDataException("Email and password can't be empty");
        
        var user = await repository.GetByEmail(loginUser.Email);

        if (user == null)
            throw new NotFoundException("User");

        if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            throw new InvalidPasswordException();
        
        var response = new AuthorizationDto()
        {
            Id = user.Id,
            Token = "",
        };
        
        return response;
    }

    public async Task<List<GetUserDto>> GetAll()
    {
        var users = await repository.GetAll();
        
        return users.Select(UserMapping.ToGetUserDto).ToList();
    }

    public async Task<GetUserDto> GetById(uint id)
    {
        await Exist(id);
        
        return UserMapping.ToGetUserDto(await repository.GetById(id));
    }

    public async Task Update(string name, uint id)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new InvalidInputDataException("Name can't be empty");

        await Exist(id);
        
        await repository.Update(name, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task Block(uint id)
    {
        await Exist(id);
        
        await repository.Block(id);
    }

    public async Task UnBlock(uint id)
    {
        await Exist(id);
        
        await repository.UnBlock(id);
    }

    public async Task MakeAdmin(uint id)
    {
        await Exist(id);
        
        await repository.MakeAdmin(id);
    }

    public async Task MakeUser(uint id)
    {
        await Exist(id);
        
        await repository.MakeUser(id);
    }

    public async Task Exist(uint id)
    {
        if (!await repository.Exist(id))
            throw new NotFoundException("User");
    }

    private async Task Exist(string email)
    {
        if(await repository.Exist(email))
            throw new AlreadyAddedException("User");
    }
}