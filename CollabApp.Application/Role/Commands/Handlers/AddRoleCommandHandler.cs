using CollabApp.Application.Role.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Role;
using MediatR;

namespace CollabApp.Application.Role.Commands.Handlers
{
    public class AddRoleCommandHandler(IRoleService roleService) : IRequestHandler<AddRoleCommand,Result<RoleResponseDetail>>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result<RoleResponseDetail>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.AddAsync(request.RoleRequest);
        }
    }
}
