using Coursework.Application.Authorization.Requirement;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Coursework.Infrastructure.Jwt.Authorization.Handlers;

public class OwnerTemplateForFormHandler(IFormRepository formRepository) :  AuthorizationHandler<OwnerTemplateForFormRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerTemplateForFormRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext) return;

        var routeValues = httpContext.GetRouteData().Values;
        if (!routeValues.TryGetValue("id", out var idValue) || idValue is not string idString)
            return;

        if (!uint.TryParse(idString, out var entityId))
            return;
        
        var userIdClaim = context.User.FindFirst("id");
        if(userIdClaim is null || string.IsNullOrEmpty(userIdClaim.Value))
            return;

        if (!await formRepository.Exist(entityId))
            throw new NotFoundException("Form");
        
        var form = await formRepository.GetById(entityId);
        
        if (form.Template.AuthorId.ToString() == userIdClaim.Value)
        {
            context.Succeed(requirement);
        }
    }
}