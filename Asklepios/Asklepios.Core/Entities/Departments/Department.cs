using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Entities.Departments;

public class Department
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int NumberOfBeds { get; set; }
    public int ActualNumberOfPatients { get; set; }
    public IEnumerable<Room> Rooms { get; set; }
    public IEnumerable<Patient> Patients { get; set; }
    
    public Nurse Nurse { get; set; }
    public MedicalStaff MedicalStaff { get; set; }

    public Department(Guid departmentId, string departmentName, int numberOfBeds, int actualNumberOfPatients)
    {
        DepartmentId = departmentId;
        DepartmentName = departmentName;
        NumberOfBeds = numberOfBeds;
        ActualNumberOfPatients = actualNumberOfPatients;
    }

    public Department()
    {
    }
}