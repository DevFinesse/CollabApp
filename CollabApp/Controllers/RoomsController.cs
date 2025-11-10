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
    public class RoomsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoomRequest roomRequest, CancellationToken cancellationToken)
        {
           var result =  await _mediator.Send(new CreateRoomCommand(User.GetUserId()!, roomRequest), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoomByIdQuery(Id));
            return Ok(result);
        }

    }
}
