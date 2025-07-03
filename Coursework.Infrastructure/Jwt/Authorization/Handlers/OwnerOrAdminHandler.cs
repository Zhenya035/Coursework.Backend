using Coursework.Application.Authorization.Requirement;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Domain.Enums;
using Coursework.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Coursework.Infrastructure.Jwt.Authorization.Handlers;

public class OwnerOrAdminHandler<TEntry>(IOwnerService<TEntry> service) : AuthorizationHandler<OwnerOrAdminRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrAdminRequirement ownerOrAdminRequirement)
    {
        var roleClaim = context.User.FindFirst("role");
        
        if (roleClaim is not null && Enum.TryParse<RoleEnum>(roleClaim.Value, out var roleEnum) && roleEnum == RoleEnum.Admin)
        {
            context.Succeed(ownerOrAdminRequirement);
            return;
        }
        
        if (context.Resource is not HttpContext httpContext) return;

        var routeValues = httpContext.GetRouteData().Values;

        var idParamName = ownerOrAdminRequirement.IdParameterName;
        
        if (!routeValues.TryGetValue(idParamName, out var idValue) || idValue is not string idString)
            return;

        if (!uint.TryParse(idString, out var entityId))
            return;
        
        if (typeof(TEntry) == typeof(Comment) && idParamName != "commentId") return;
        if (typeof(TEntry) == typeof(Form) && idParamName != "formId") return;
        if (typeof(TEntry) == typeof(Template) && idParamName != "templateId") return;
        
        var userIdClaim = context.User.FindFirst("id");
        if(userIdClaim is null || string.IsNullOrEmpty(userIdClaim.Value))
            return;

        var ownerId = await service.GetOwnerId(entityId);
        if(ownerId == null)
            return;
        
        if (ownerId.ToString() == userIdClaim.Value)
        {
            context.Succeed(ownerOrAdminRequirement);
        }
    }
}