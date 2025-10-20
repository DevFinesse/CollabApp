namespace CollabApp.Shared.Dtos.Role
{
    public record RoleRequest(
    string Name,
    List<string> Permissions
);
}
