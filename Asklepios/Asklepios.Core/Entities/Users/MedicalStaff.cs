using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Entities.Users;

public class MedicalStaff
{
    public Guid DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PrivatePhoneNumber { get; set; }
    public string HospitalPhoneNumber { get; set; }
    public string Specialization { get; set; }
    public string MedicalLicenseNumber { get; set; }
    
    public Guid UserId { get; set; } 
    public User User { get; set; }
    
    public Guid DepartmentId { get; set; } 
    public Department Department { get; set; }
}