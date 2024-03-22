namespace Asklepios.Core.Entities.Departments;

public class Department
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int NumberOfBeds { get; set; }
    public int NumberOfPatients { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
}