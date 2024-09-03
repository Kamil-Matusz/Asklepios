namespace Asklepios.Core.Entities.Patients;

public class PatientHistory
{
    public Guid HistoryId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string History { get; set; }
}