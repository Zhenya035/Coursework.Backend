using Coursework.Application.Authorization.Requirement;
using Coursework.Application.Interfaces.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Coursework.Infrastructure.Jwt.Authorization.Handlers;

public class OwnerHandler<TEntry>(IOwnerService<TEntry> service) : AuthorizationHandler<OwnerRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return;

        var routeValues = httpContext.GetRouteData().Values;
        if (!routeValues.TryGetValue("id", out var idValue) || idValue is not string idString)
            return;

        if (!uint.TryParse(idString, out var entityId))
            return;
        
        var userId = context.User.FindFirst("id")!.Value;
        if(string.IsNullOrEmpty(userId))
            return;

        var ownerId = await service.GetOwnerId(entityId);
        if(ownerId == null)
            return;
        
        if (ownerId.ToString() == userId)
        {
            context.Succeed(requirement);
        }
    }
}