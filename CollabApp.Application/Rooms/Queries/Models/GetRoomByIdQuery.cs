using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Rooms.Queries.Models
{
    public record GetRoomByIdQuery(Guid RoomId) : IRequest<Result<RoomResponse>>;
}
