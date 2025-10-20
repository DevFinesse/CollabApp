namespace CollabApp.Shared.Abstractions.Consts
{
    public static class Permissions
    {
        public static string Type { get; } = "permissions";

        public const string GetUsers = "users:read";
        public const string AddUsers = "users:add";
        public const string UpdateUsers = "users:update";

        public static IList<string?> GetAllPermissions()
        {
            return typeof(Permissions).GetFields().Select(x => x.GetValue(x) as string).ToList();
        }
    }
}
