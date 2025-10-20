using Microsoft.AspNetCore.Authorization;

namespace CollabApp.Infrastructure.Authentication.Permission;
public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
	public string Permission { get; } = permission;
}