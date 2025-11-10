using CollabApp.Application.Users.Commands.Models;
using CollabApp.Application.Users.Queries.Models;
using CollabApp.Extensions;
using CollabApp.Shared.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollabApp.Controllers
{
    [Route("api/me")]
    [ApiController]
    [Authorize]
    public class AccountsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("")]
        public async Task<IActionResult> Info()
        {
            var result = await _mediator.Send(new GetProfileInfoQuery(User.GetUserId()!));
            return Ok(result.Value);
        }

        [HttpPut("info")]
        public async Task<IActionResult> Info([FromBody] UpdateProfileRequest request)
        {
            await _mediator.Send(new UpdateProfileCommand(User.GetUserId()!, request));
            return NoContent();
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _mediator.Send(new ChangePasswordCommand(User.GetUserId()!, request));
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }

    
}
