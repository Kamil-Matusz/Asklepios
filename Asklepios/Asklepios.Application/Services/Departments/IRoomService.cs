using Asklepios.Core.DTO.Departments;

namespace Asklepios.Application.Services.Departments;

public interface IRoomService
{
    Task AddRoomAsync(RoomDto dto);
    Task<RoomDto> GetRoomAsync(Guid id);
    Task<IReadOnlyList<RoomListDto>> GetAllRoomsAsync(int pageIndex, int pageSize);
    Task UpdateRoomAsync(RoomDto dto);
    Task DeleteRoomAsync(Guid id);
}