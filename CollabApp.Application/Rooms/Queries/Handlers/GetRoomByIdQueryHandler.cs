using CollabApp.Application.Rooms.Queries.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using MediatR;

namespace CollabApp.Application.Rooms.Queries.Handlers
{
    public class GetRoomByIdQueryHandler(IRoomService roomService) : IRequestHandler<GetRoomByIdQuery, Result<RoomResponse>>
    {
        private readonly IRoomService _roomService = roomService;
        public async Task<Result<RoomResponse>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await _roomService.GetAsync(request.RoomId, cancellationToken);
        }
    }
}
