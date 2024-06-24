using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Policies.Departments;

public interface IRoomDeletePolicy
{
    Task<bool> CannotDeleteRoomPolicy(Room room);
}