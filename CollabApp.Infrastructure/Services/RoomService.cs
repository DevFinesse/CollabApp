using CollabApp.Contracts.Repository;
using CollabApp.Contracts.Services;
using CollabApp.Domain.Entities;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CollabApp.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Result<RoomResponse>> AddAsync(string userId, RoomRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    return Result.Failure<RoomResponse>(new Error(
                        "Room.InvalidName",
                        "Room name is required",
                        StatusCodes.Status400BadRequest));
                }

                var room = new Room
                {
                    Name = request.Name,
                    Description = request.Description,
                    CreatedBy = userId,
                    CreatedAt = DateTime.UtcNow
                };

                await _roomRepository.AddAsync(room, cancellationToken);
                
                var response = new RoomResponse(
                    Id: room.Id,
                    Name: room.Name,
                    Description: room.Description,
                    CreatedBy: room.CreatedBy,
                    CreatedAt: room.CreatedAt
                );

                return Result.Success(response);
            }
            catch (Exception ex)
            {
                return Result.Failure<RoomResponse>(new Error(
                    "Room.AddError",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
            }
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Result.Failure(new Error(
                        "Room.InvalidId",
                        "Invalid room ID provided",
                        StatusCodes.Status400BadRequest));
                }

                var room = await _roomRepository.GetAsQueryable()
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (room is null)
                {
                    return Result.Failure(new Error(
                        "Room.NotFound",
                        $"Room with ID {id} was not found",
                        StatusCodes.Status404NotFound));
                }

                await _roomRepository.DeleteAsync(room, cancellationToken);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(
                    "Room.DeleteError",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
            }
        }

        public async Task<Result<RoomResponse>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Result.Failure<RoomResponse>(new Error(
                        "Room.InvalidId",
                        "Invalid room ID provided",
                        StatusCodes.Status400BadRequest));
                }

                var room = await _roomRepository.GetAsQueryable()
                    .Where(x => x.Id == id)
                    .Select(x => new RoomResponse(
                        x.Id,
                        x.Name,
                        x.Description,
                        x.CreatedBy,
                        x.CreatedAt
                    ))
                    .SingleOrDefaultAsync(cancellationToken);

                if (room is null)
                {
                    return Result.Failure<RoomResponse>(new Error(
                        "Room.NotFound",
                        $"Room with ID {id} was not found",
                        StatusCodes.Status404NotFound));
                }

                return Result.Success(room);
            }
            catch (Exception ex)
            {
                return Result.Failure<RoomResponse>(new Error(
                    "Room.GetError",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
            }
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Result.Failure(new Error(
                        "Room.InvalidId",
                        "Invalid room ID provided",
                        StatusCodes.Status400BadRequest));
                }

                var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

                if (room is null)
                {
                    return Result.Failure(new Error(
                        "Room.NotFound",
                        $"Room with ID {id} was not found",
                        StatusCodes.Status404NotFound));
                }

                // Update room properties
                room.Name = request.Name ?? room.Name;
                room.Description = request.Description ?? room.Description;

                await _roomRepository.UpdateAsync(room, cancellationToken);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(
                    "Room.UpdateError",
                    ex.Message,
                    StatusCodes.Status500InternalServerError));
            }
        }
    }
}
