using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Exceptions.Departments;
using Asklepios.Core.Repositories.Departments;

namespace Asklepios.Application.Services.Departments;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task AddRoomAsync(RoomDto dto)
    {
        dto.RoomId = Guid.NewGuid();
        await _roomRepository.AddRoomAsync(new Room
        {
            RoomId = dto.RoomId,
            DepartmentId = dto.DepartmentId,
            RoomNumber = dto.RoomNumber,
            RoomType = dto.RoomType,
            NumberOfBeds = dto.NumberOfBeds
        });
    }

    public async Task<RoomDto> GetRoomAsync(Guid id)
    {
        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room is null)
        {
            return null;
        }

        var dto = Map<RoomDto>(room);

        return dto;
    }

    public async Task<IReadOnlyList<RoomListDto>> GetAllRoomsAsync(int pageIndex, int pageSize)
    {
        var rooms = await _roomRepository.GetAllRoomsAsync(pageIndex, pageSize);
        return rooms.Select(MapRoomList<RoomListDto>).ToList();
    }

    public async Task UpdateRoomAsync(RoomDto dto)
    {
        var room = await _roomRepository.GetRoomByIdAsync(dto.RoomId);
        if (room is null)
        {
            throw new RoomNotFoundException(dto.RoomId);
        }

        room.RoomNumber = dto.RoomNumber;
        room.RoomType = dto.RoomType;

        await _roomRepository.UpdateRoomAsync(room);
    }

    public async Task DeleteRoomAsync(Guid id)
    {
        var room = await _roomRepository.GetRoomByIdAsync(id);
        if (room is null)
        {
            throw new RoomNotFoundException(id);
        }
        
        await _roomRepository.DeleteRoomAsync(room);
    }

    private static T Map<T>(Room room) where T : RoomDto, new() => new T()
    {
        RoomId = room.RoomId,
        DepartmentId = room.DepartmentId,
        RoomNumber = room.RoomNumber,
        RoomType = room.RoomType,
        NumberOfBeds = room.NumberOfBeds
    };
    
    private static T MapRoomList<T>(Room room) where T : RoomListDto, new() => new T()
    {
        RoomId = room.RoomId,
        DepartmentName = room.Department.DepartmentName,
        RoomNumber = room.RoomNumber,
        RoomType = room.RoomType,
        NumberOfBeds = room.NumberOfBeds
    };
}