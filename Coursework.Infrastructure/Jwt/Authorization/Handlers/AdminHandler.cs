using Coursework.Application.Authorization.Requirement;
using Coursework.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Coursework.Infrastructure.Jwt.Authorization.Handlers;

public class AdminHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminRequirement requirement)
    {
        var roleClaim = context.User.FindFirst("role");
        
        if (roleClaim is null || !Enum.TryParse<RoleEnum>(roleClaim.Value, out var roleEnum) || roleEnum != RoleEnum.Admin)
        {
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }

}