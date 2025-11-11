using CollabApp.Application.Rooms.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Rooms.Commands.Handlers
{
    public class DeleteRoomCommandHandler(IRoomService roomService) : IRequestHandler<DeleteRoomCommand, Result>
    {
        private readonly IRoomService _roomService = roomService;

        public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            return await _roomService.DeleteAsync(request.RoomId, cancellationToken);
        }
    }
}
