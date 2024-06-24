using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.Entities.Examinations;

public class Operation
{
    public Guid OperationId { get; set; }
    public string OperationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public AnesthesiaType AnesthesiaType { get; set; }
    public string Result { get; set; }
    public string Comlications { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    
    public Guid MedicalStaffId { get; set; }
    public MedicalStaff MedicalStaff { get; set; }
}