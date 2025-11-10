using CollabApp.Contracts.Repository;
using Microsoft.EntityFrameworkCore;

namespace CollabApp.Infrastructure.Persistence.Repository
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;
        public async Task DeleteRolesOfUserAsync(string userId, CancellationToken cancellationToken)
        {
            await _context.UserRoles
                .Where(x => x.UserId.Equals(userId))
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
