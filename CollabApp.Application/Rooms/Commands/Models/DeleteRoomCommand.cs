using CollabApp.Shared.Abstractions;
using MediatR;

namespace CollabApp.Application.Rooms.Commands.Models
{
    public record DeleteRoomCommand(Guid RoomId, CancellationToken CancellationToken) : IRequest<Result>;
}
