using CollabApp.Application.Role.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Role.Commands.Handlers
{
    public class UpdateRoleCommandHandler(IRoleService roleService) : IRequestHandler<UpdateRoleCommand , Result>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.UpdateAsync(request.Id, request.RoleRequest);
        }
    }
}
