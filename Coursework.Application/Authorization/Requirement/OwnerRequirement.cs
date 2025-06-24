using Microsoft.AspNetCore.Authorization;

namespace Coursework.Application.Authorization.Requirement;

public class OwnerRequirement(string idParameterName) : IAuthorizationRequirement
{
    public string IdParameterName => idParameterName;
}