using CollabApp.Application.Role.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Commands.Handlers
{
    public class ToggleRoleStatusCommandHandler(IRoleService roleService) : IRequestHandler<ToggleRoleStatusCommand, Result>
    {
        private readonly IRoleService _roleService = roleService;

        public async Task<Result> Handle(ToggleRoleStatusCommand request, CancellationToken cancellationToken)
        {
            return await _roleService.ToggleStatusAsync(request.Id);
        }
    }
}
