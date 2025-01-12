using Microsoft.AspNetCore.Authorization;

namespace sayHello.api.Authorization;

[AttributeUsage(AttributeTargets.Method )]
public class RequirePermissionAttribute : AuthorizeAttribute
{
        public Permissions Permission { get; }

        public RequirePermissionAttribute(Permissions permission) 
                : base(permission.ToString())  
        {
                Permission = permission;
        }
}