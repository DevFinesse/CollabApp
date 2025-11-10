using CollabApp.Contracts.Repository;
using CollabApp.Domain.Entities;
using System.Linq.Expressions;

namespace CollabApp.Infrastructure.Persistence.Repository
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context):base(context)
        { }
    }
}
