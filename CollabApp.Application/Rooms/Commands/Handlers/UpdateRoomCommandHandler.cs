using CollabApp.Application.Rooms.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using MediatR;


namespace CollabApp.Application.Rooms.Commands.Handlers
{
    public class UpdateRoomCommandHandler(IRoomService roomService) : IRequestHandler<UpdateRoomCommand , Result>
    {
        private readonly IRoomService _roomService = roomService;

        public async Task<Result> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            return await _roomService.UpdateAsync(request.RoomId, request.UpdateRoomRequest, cancellationToken);
        }
    }
}
