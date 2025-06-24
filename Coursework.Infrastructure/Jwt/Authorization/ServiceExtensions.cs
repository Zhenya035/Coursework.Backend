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
            options.AddPolicy("OwnerOnly.Universal", policy =>
                policy.AddRequirements(new OwnerRequirement("id")));
            options.AddPolicy("OwnerOnly.Template", policy =>
                policy.AddRequirements(new OwnerRequirement("templateId")));
            options.AddPolicy("OwnerTemplateForForm", policy =>
                policy.AddRequirements(new OwnerTemplateForFormRequirement()));
        });

        services.AddSingleton<IAuthorizationHandler, AdminHandler>();
        services.AddScoped<IAuthorizationHandler, OwnerTemplateForFormHandler>();
        services.AddScoped<IAuthorizationHandler, OwnerHandler<Comment>>();
        services.AddScoped<IAuthorizationHandler, OwnerHandler<Form>>();
        services.AddScoped<IAuthorizationHandler, OwnerHandler<Template>>();
        services.AddScoped<IAuthorizationHandler, OwnerHandler<Like>>();
        
        services.AddScoped<IOwnerService<Comment>, CommentService>();
        services.AddScoped<IOwnerService<Form>, FormService>();
        services.AddScoped<IOwnerService<Template>, TemplateService>();
        services.AddScoped<IOwnerService<Like>, LikeService>();
    }
}