using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using MediatR;

namespace CollabApp.Application.Rooms.Commands.Models
{
    public record CreateRoomCommand(string UserId, RoomRequest Request) : IRequest<Result<RoomResponse>>;
}
