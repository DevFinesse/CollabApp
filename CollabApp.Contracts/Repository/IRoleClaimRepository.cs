
using CollabApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CollabApp.Contracts.Repository
{
    public interface IRoleClaimRepository
    {
        Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesPermissions(User user, CancellationToken cancellationToken);
        Task RemoveClaimsAsync(string roleId, IEnumerable<string> claimValues, CancellationToken cancellationToken = default);
        Task AddClaimsAsync(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken = default);
        Task<List<string>> GetClaimsOfRoleAsync(string roleId);
    }
}
