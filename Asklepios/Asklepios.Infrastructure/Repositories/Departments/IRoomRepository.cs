using Asklepios.Core.Entities.Departments;

namespace Asklepios.Infrastructure.Repositories.Departments;

public interface IRoomRepository
{
    Task<Room> GetRoomByIdAsync(Guid roomId);
    Task<IReadOnlyList<Room>> GetAllRoomsAsync();
    Task AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(Room room);
}