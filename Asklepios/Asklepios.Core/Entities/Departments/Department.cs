using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Entities.Departments;

public class Department
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int NumberOfBeds { get; set; }
    public int ActualNumberOfPatients { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    
    public Nurse Nurse { get; set; }
    public MedicalStaff MedicalStaff { get; set; }
}