using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using MediatR;

namespace CollabApp.Application.Rooms.Commands.Models
{
    public record UpdateRoomCommand(Guid RoomId, UpdateRoomRequest UpdateRoomRequest, CancellationToken CancellationToken) : IRequest<Result>;
}
