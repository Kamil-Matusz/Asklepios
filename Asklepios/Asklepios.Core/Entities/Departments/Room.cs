namespace Asklepios.Core.Entities.Departments;

public class Room
{
    public Guid RoomId { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
}