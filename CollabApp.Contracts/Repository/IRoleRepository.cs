namespace CollabApp.Contracts.Repository
{
    public interface IRoleRepository
    {
        Task DeleteRolesOfUserAsync(string userId, CancellationToken cancellationToken);
    }
}
