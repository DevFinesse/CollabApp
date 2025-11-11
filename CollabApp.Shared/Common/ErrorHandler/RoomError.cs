using CollabApp.Shared.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Shared.Common.ErrorHandler
{
    public static class RoomError
    {
        public static readonly Error RoomNotFound =
            new("Room.Notfound", "Room not found", StatusCodes.Status404NotFound);

        public static readonly Error InvalidPermissions =
            new("Room.InvalidPermissions", "Invalid permissions", StatusCodes.Status400BadRequest);
    }
}
