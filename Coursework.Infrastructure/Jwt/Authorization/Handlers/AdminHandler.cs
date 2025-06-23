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
        var role = context.User.FindFirst("role")!.Value;
        
        if (Enum.TryParse<RoleEnum>(role, out var roleEnum) && roleEnum == RoleEnum.Admin)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

}