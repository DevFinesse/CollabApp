using CollabApp.Shared.Dtos.Chat;
using CollabApp.Shared.Dtos.RoomMember;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Room
{
    public record UpdateRoomRequest
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
    }
}
