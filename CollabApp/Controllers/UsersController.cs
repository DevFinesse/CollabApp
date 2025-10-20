using CollabApp.Application.Users.Commands.Models;
using CollabApp.Extensions;
using CollabApp.Infrastructure.Authentication.Permission;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Abstractions.Consts;
using CollabApp.Shared.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollabApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        //[HasPermission(Permissions.AddUsers)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserCommand(request), cancellationToken);
            return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            // Placeholder for Get method - implement as needed
            return Ok(new { message = "Get method not implemented yet" });
        }
    }
}
