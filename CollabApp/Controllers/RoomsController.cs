using CollabApp.Application.Rooms.Commands.Models;
using CollabApp.Application.Rooms.Queries.Models;
using CollabApp.Extensions;
using CollabApp.Shared.Dtos.Room;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollabApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRoomRequest roomRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateRoomCommand(User.GetUserId()!, roomRequest), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomByIdQuery(Id), cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRoomRequest roomRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateRoomCommand(Id, roomRequest, cancellationToken));
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteRoomCommand(Id, cancellationToken));
            return Ok(result);
        }

    }
}
