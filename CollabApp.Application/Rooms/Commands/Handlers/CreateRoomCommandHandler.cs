using CollabApp.Application.Rooms.Commands.Models;
using CollabApp.Contracts.Services;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollabApp.Application.Rooms.Commands.Handlers
{
    public class CreateRoomCommandHandler(IRoomService roomService) : IRequestHandler<CreateRoomCommand, Result<RoomResponse>>
    {
        private readonly IRoomService _roomService = roomService;
        
        public async Task<Result<RoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            return await _roomService.AddAsync(request.UserId, request.Request, cancellationToken);
        }
    }
}
