using Microsoft.AspNetCore.Authorization;

namespace sayHello.api.Authorization;

public class PermissionRequirement : IAuthorizationRequirement
{
    public Permissions Permission { get; }

    public PermissionRequirement(Permissions permission)
    {
        Permission = permission;
    }
}