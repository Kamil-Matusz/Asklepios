using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Entities.Patients;

public class Patient
{
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string InitialDiagnosis { get; set; }
    public bool IsDischarged { get; set; }
    public string Treatment { get; set; }
    
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    
    public Guid RoomId { get; set; }
    public Room Room { get; set; }

    public Guid MedicalStaffId { get; set; }
    public MedicalStaff MedicalStaff { get; set; }
    
    public IEnumerable<ExamResult> ExamResults { get; set; }
    
    public IEnumerable<Operation> Operations { get; set; }
    
    public DateOnly AdmissionDate { get; set; }
    
    public string Address { get; set; }

    public Patient()
    {
    }

    public Patient(Guid patientId, string patientName, string patientSurname, string peselNumber, string initialDiagnosis, bool isDischarged, string treatment, Guid departmentId, Guid roomId, Guid medicalStaffId, DateOnly admissionDate, string address)
    {
        PatientId = patientId;
        PatientName = patientName;
        PatientSurname = patientSurname;
        PeselNumber = peselNumber;
        InitialDiagnosis = initialDiagnosis;
        IsDischarged = isDischarged;
        Treatment = treatment;
        DepartmentId = departmentId;
        RoomId = roomId;
        MedicalStaffId = medicalStaffId;
        AdmissionDate = admissionDate;
        Address = address;
    }
}