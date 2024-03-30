using Asklepios.Core.Entities.Departments;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Asklepios.Infrastructure.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories;

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
            .SingleOrDefaultAsync(x => x.RoomId == roomId);

    public async Task<IReadOnlyList<Room>> GetAllRoomsAsync()
        => await _rooms
            .AsNoTracking()
            .Include(x => x.Department)
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
}