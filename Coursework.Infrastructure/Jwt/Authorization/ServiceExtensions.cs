using Coursework.Application.Authorization.Requirement;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Application.Services;
using Coursework.Domain.Models;
using Coursework.Infrastructure.Jwt.Authorization.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Coursework.Infrastructure.Jwt.Authorization;

public static class ServiceExtensions
{
    public static void AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => 
                policy.AddRequirements(new AdminRequirement()));
            options.AddPolicy("OwnerOrAdminOnly.Comment", policy =>
                policy.AddRequirements(new OwnerOrAdminRequirement("commentId")));
            options.AddPolicy("OwnerOrAdminOnly.Form", policy =>
                policy.AddRequirements(new OwnerOrAdminRequirement("formId")));
            options.AddPolicy("OwnerOrAdminOnly.Like", policy =>
                policy.AddRequirements(new OwnerOrAdminRequirement("likeId")));
            options.AddPolicy("OwnerOrAdminOnly.Template", policy =>
                policy.AddRequirements(new OwnerOrAdminRequirement("templateId")));
        });

        services.AddSingleton<IAuthorizationHandler, AdminHandler>();
        services.AddScoped<IAuthorizationHandler, OwnerOrAdminHandler<Comment>>();
        services.AddScoped<IAuthorizationHandler, OwnerOrAdminHandler<Form>>();
        services.AddScoped<IAuthorizationHandler, OwnerOrAdminHandler<Template>>();
        services.AddScoped<IAuthorizationHandler, OwnerOrAdminHandler<Like>>();
        
        services.AddScoped<IOwnerService<Comment>, CommentService>();
        services.AddScoped<IOwnerService<Form>, FormService>();
        services.AddScoped<IOwnerService<Template>, TemplateService>();
        services.AddScoped<IOwnerService<Like>, LikeService>();
    }
}