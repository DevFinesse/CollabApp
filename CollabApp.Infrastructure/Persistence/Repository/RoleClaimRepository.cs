using CollabApp.Contracts.Repository;
using CollabApp.Domain.Entities;
using CollabApp.Infrastructure.Persistence;
using CollabApp.Shared.Abstractions.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollabApp.Infrastructure.Persistence.Repository
{
    public class RoleClaimRepository(UserManager<User> userManager,
        ApplicationDbContext context,
        RoleManager<ApplicationRole> roleManager
        ) : IRoleClaimRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;


        public async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesPermissions(User user, CancellationToken cancellationToken)
        {
            var userRoles = await _userManager.GetRolesAsync( user );

            var userPermissions = await(
                    from r in _context.Roles
                    join p in _context.RoleClaims
                    on r.Id equals p.RoleId
                    where userRoles.Contains(r.Name!)
                    select p.ClaimValue!)
                    .Distinct()
                    .ToListAsync(cancellationToken);

            return (userRoles, userPermissions);
        }

        public async Task AddClaimsAsync(IEnumerable<IdentityRoleClaim<string>> claims, CancellationToken cancellationToken = default)
        {
            await _context.RoleClaims.AddRangeAsync(claims, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<string>> GetClaimsOfRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            var currentPermissions = await _context.RoleClaims
                        .Where(x => x.RoleId == role!.Id && x.ClaimType == Permissions.Type)
                        .Select(x => x.ClaimValue)
                        .ToListAsync();

            return currentPermissions!;
        }

       

        public async Task RemoveClaimsAsync(string roleId, IEnumerable<string> claimValues, CancellationToken cancellationToken = default)
        {

            await _context.RoleClaims
                .Where(x => x.RoleId == roleId && claimValues.Contains(x.ClaimValue))
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
