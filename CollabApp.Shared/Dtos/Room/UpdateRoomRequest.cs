using CollabApp.Shared.Dtos.Chat;
using CollabApp.Shared.Dtos.RoomMember;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Dtos.Room
{
    public record UpdateRoomRequest
    (string Name, string Description);
}
