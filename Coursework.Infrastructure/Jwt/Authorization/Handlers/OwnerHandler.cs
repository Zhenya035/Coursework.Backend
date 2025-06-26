using Coursework.Application.Authorization.Requirement;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Domain.Models;
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

        var idParamName = requirement.IdParameterName;
        
        if (!routeValues.TryGetValue(idParamName, out var idValue) || idValue is not string idString)
            return;

        if (!uint.TryParse(idString, out var entityId))
            return;
        
        if (typeof(TEntry) == typeof(Comment) && idParamName != "commentId") return;
        if (typeof(TEntry) == typeof(Form) && idParamName != "formId") return;
        if (typeof(TEntry) == typeof(Template) && idParamName != "templateId") return;
        if (typeof(TEntry) == typeof(Like) && idParamName != "likeId") return;
        
        var userIdClaim = context.User.FindFirst("id");
        if(userIdClaim is null || string.IsNullOrEmpty(userIdClaim.Value))
            return;

        var ownerId = await service.GetOwnerId(entityId);
        if(ownerId == null)
            return;
        
        if (ownerId.ToString() == userIdClaim.Value)
        {
            context.Succeed(requirement);
        }
    }
}