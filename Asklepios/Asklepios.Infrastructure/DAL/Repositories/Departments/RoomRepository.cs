using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Repositories.Departments;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Departments;

public class RoomRepository : IRoomRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Room> _rooms;

    public RoomRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _rooms = _dbContext.Rooms;
    }

    public Task<Room> GetRoomByIdAsync(Guid roomId)
        => _rooms
            .AsNoTracking()
            .Include(x => x.Department)
            .Include(x => x.Patients)
            .SingleOrDefaultAsync(x => x.RoomId == roomId);

    public async Task<IReadOnlyList<Room>> GetAllRoomsAsync(int pageIndex, int pageSize)
        => await _rooms
            .AsNoTracking()
            .Include(x => x.Department)
            .OrderBy(x => x.RoomNumber)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddRoomAsync(Room room)
    {
        await _rooms.AddAsync(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRoomAsync(Room room)
    {
        _rooms.Update(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoomAsync(Room room)
    {
        _rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CountPatientsInRoomAsync(Guid roomId)
    {
        var room = await _rooms
            .Include(d => d.Patients)
            .FirstOrDefaultAsync(d => d.RoomId == roomId);

        return room?.Patients.Count() ?? 0;
    }

    public async Task<int> GetNumberOfBedsAsync(Guid roomId)
    {
        var room = await _rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.RoomId == roomId);

        return room?.NumberOfBeds ?? 0;
    }
}