using System.Text;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Infrastructure.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Coursework.WebAPI.Extensions;

public static class AuthExtensions
{
    public static IServiceCollection AddJwtTokens(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
        services.AddSingleton<IJwtService, JwtService>();
        
        var authSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();
        
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey))
                    };
                    o.MapInboundClaims = false;
                }
            );
        
        return services;
    }
}