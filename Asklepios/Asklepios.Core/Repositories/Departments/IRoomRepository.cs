using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Repositories.Departments;

public interface IRoomRepository
{
    Task<Room> GetRoomByIdAsync(Guid roomId);
    Task<IReadOnlyList<Room>> GetAllRoomsAsync(int pageIndex, int pageSize);
    Task AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task DeleteRoomAsync(Room room);
}