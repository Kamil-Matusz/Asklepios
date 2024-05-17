using Asklepios.Core.DTO.Departments;

namespace Asklepios.Application.Services.Departments;

public interface IRoomService
{
    Task AddRoomAsync(RoomDto dto);
    Task<RoomDetailsDto> GetRoomAsync(Guid id);
    Task<IReadOnlyList<RoomListDto>> GetAllRoomsAsync(int pageIndex, int pageSize);
    Task UpdateRoomAsync(RoomDto dto);
    Task DeleteRoomAsync(Guid id);
}