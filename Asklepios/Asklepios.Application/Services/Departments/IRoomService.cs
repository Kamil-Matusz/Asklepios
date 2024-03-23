using Asklepios.Core.DTO.Departments;

namespace Asklepios.Application.Services.Departments;

public interface IRoomService
{
    Task AddRoomAsync(RoomDto dto);
    Task<RoomDto> GetRoomAsync(Guid id);
    Task<IReadOnlyList<RoomDto>> GetAllRoomsAsync();
    Task UpdateRoomAsync(RoomDto dto);
    Task DeleteRoomAsync(Guid id);
}