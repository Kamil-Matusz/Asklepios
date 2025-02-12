using Asklepios.Core.DTO.Departments;

namespace Asklepios.Core.Repositories.Departments;

public interface IRoomCacheRepository
{
    Task<IReadOnlyList<RoomListDto>?> GetRoomsAsync(int pageIndex, int pageSize);
    Task SetRoomsAsync(int pageIndex, int pageSize, IReadOnlyList<RoomListDto> rooms);
}