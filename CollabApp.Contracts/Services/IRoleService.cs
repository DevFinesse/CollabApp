using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Role;

namespace CollabApp.Contracts.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
        Task<Result<RoleResponseDetail>> GetAsync(string id);
        Task<Result<RoleResponseDetail>> AddAsync(RoleRequest request);
        Task<Result> UpdateAsync(string id, RoleRequest request);
        Task<Result> ToggleStatusAsync(string id);
    }
}
