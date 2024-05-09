using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Repositories.Departments;

public class InMemoryRoomRepository : IRoomRepository
{
    private readonly List<Room> _rooms = new();

    public Task<Room> GetRoomByIdAsync(Guid roomId)
        => Task.FromResult(_rooms.SingleOrDefault(x => x.RoomId == roomId));

    public async Task<IReadOnlyList<Room>> GetAllRoomsAsync(int pageIndex, int pageSize)
    {
        await Task.CompletedTask;
        return _rooms;
    }

    public Task AddRoomAsync(Room room)
    {
        _rooms.Add(room);
        return Task.CompletedTask;
    }

    public Task UpdateRoomAsync(Room room)
    {
        return Task.CompletedTask;
    }

    public Task DeleteRoomAsync(Room room)
    {
        _rooms.Remove(room);
        return Task.CompletedTask;
    }

    public async Task<int> CountPatientsInRoomAsync(Guid roomId)
    {
        int totalPatients = 0;

        foreach (var room in _rooms)
        {
            if (room.Patients != null)
            {
                totalPatients += room.Patients.Count();
            }
        }
        
        return totalPatients;
    }

    public async Task<int> GetNumberOfBedsAsync(Guid roomId)
    {
        var room = _rooms.FirstOrDefault(d => d.RoomId == roomId);
        return room?.NumberOfBeds ?? 0;
    }
}