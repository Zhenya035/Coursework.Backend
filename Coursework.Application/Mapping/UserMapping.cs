using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Domain.Models;

namespace Coursework.Application.Mapping;

public static class UserMapping
{
    public static User FromLoginDto(RegisterUserDto registerDto) =>
        new User()
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = registerDto.Password
        };

    public static GetUserDto ToGetUserDto(User user) =>
        new GetUserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Status = user.Status.ToString(),
            Role = user.Role.ToString(),
            TemplatesCount = user.Templates.Count
        };
}