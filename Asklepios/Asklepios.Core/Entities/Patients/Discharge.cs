using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Entities.Patients;

public class Discharge
{
    public Guid DischargeId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string Address { get; set; }
    public DateOnly Date { get; set; }
    public string DischargeReasson { get; set; }
    public string Summary { get; set; }
    
    public Guid MedicalStaffId { get; set; }
    public MedicalStaff MedicalStaff { get; set; }

    public Discharge(Guid dischargeId, string patientName, string patientSurname, string peselNumber, string address, DateOnly date, string dischargeReasson, string summary, Guid medicalStaffId)
    {
        DischargeId = dischargeId;
        PatientName = patientName;
        PatientSurname = patientSurname;
        PeselNumber = peselNumber;
        Address = address;
        Date = date;
        DischargeReasson = dischargeReasson;
        Summary = summary;
        MedicalStaffId = medicalStaffId;
    }

    public Discharge()
    {
    }
}