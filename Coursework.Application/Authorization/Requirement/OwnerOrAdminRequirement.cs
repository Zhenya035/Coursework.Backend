using Microsoft.AspNetCore.Authorization;

namespace Coursework.Application.Authorization.Requirement;

public class OwnerOrAdminRequirement(string idParameterName) : IAuthorizationRequirement
{
    public string IdParameterName => idParameterName;
}