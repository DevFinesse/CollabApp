using Microsoft.AspNetCore.Authorization;

namespace CollabApp.Infrastructure.Authentication.Permission;
public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}