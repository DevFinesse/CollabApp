using CollabApp.Contracts.Repository;
using CollabApp.Contracts.Services;
using CollabApp.Domain.Entities;
using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Common.ErrorHandler;
using CollabApp.Shared.Dtos.Room;
using CollabApp.Shared.Dtos.User;
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

        public async Task<Result<RoomResponse>> AddAsync(string userId, CreateRoomRequest request, CancellationToken cancellationToken = default)
        {
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

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
                var room = await _roomRepository.GetAsQueryable()
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (room == null)
                return Result.Failure<RoomResponse>(RoomError.RoomNotFound);




            await _roomRepository.DeleteAsync(room, cancellationToken);
                return Result.Success();
           
        }

        public async Task<Result<RoomResponse>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
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

            if (room == null)
                return Result.Failure<RoomResponse>(RoomError.RoomNotFound);



            return Result.Success(room);
            
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateRoomRequest request, CancellationToken cancellationToken = default)
        { 
                var room = await _roomRepository.GetByIdAsync(id, cancellationToken);

            if (room == null)
                return Result.Failure<RoomResponse>(RoomError.RoomNotFound);

            // Update room properties
            room.Name = request.Name ?? room.Name;
            room.Description = request.Description ?? room.Description;

                await _roomRepository.UpdateAsync(room, cancellationToken);

                return Result.Success();
        }
    }
}
