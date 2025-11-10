using CollabApp.Application.Role.Queries.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Role.Queries.Handlers
{
    public class GetAllRolesQueryHandler(IRoleService roleService) : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleResponse>>
    {
        private readonly IRoleService _roleService = roleService;
        public async Task<IEnumerable<RoleResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleService.GetAllAsync(request.IncludeDisabled, cancellationToken);
        }

    }
}
