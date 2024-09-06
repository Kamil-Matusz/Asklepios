using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Entities.Patients;

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
    
    public IEnumerable<Discharge> Discharges { get; set; }
    
    public IEnumerable<Operation> Operations { get; set; }
    public IEnumerable<Patient> Patients { get; set; }

    public MedicalStaff()
    {
    }

    public MedicalStaff(Guid doctorId, string name, string surname, string privatePhoneNumber, string hospitalPhoneNumber, string specialization, string medicalLicenseNumber, Guid userId, Guid departmentId)
    {
        DoctorId = doctorId;
        Name = name;
        Surname = surname;
        PrivatePhoneNumber = privatePhoneNumber;
        HospitalPhoneNumber = hospitalPhoneNumber;
        Specialization = specialization;
        MedicalLicenseNumber = medicalLicenseNumber;
        UserId = userId;
        DepartmentId = departmentId;
    }
}