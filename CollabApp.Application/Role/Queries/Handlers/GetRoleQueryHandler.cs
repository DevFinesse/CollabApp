using CollabApp.Application.Role.Queries.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Queries.Handlers
{
    public class GetRoleQueryHandler(IRoleService roleService) : IRequestHandler<GetRoleQuery, Result<RoleResponseDetail>>
    {
        private readonly IRoleService _roleService = roleService;
        public async Task<Result<RoleResponseDetail>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetAsync(request.RoleId);
        }

    }
}
