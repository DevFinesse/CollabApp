using CollabApp.Shared.Abstractions;
using CollabApp.Shared.Dtos.Room;

namespace CollabApp.Contracts.Services
{
    public interface IRoomService
    {
        Task<Result<RoomResponse>> AddAsync(string UserId, RoomRequest request, CancellationToken cancellationToken = default);
        Task<Result<RoomResponse>> GetAsync(Guid Id,CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Guid Id,UpdateRoomRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
