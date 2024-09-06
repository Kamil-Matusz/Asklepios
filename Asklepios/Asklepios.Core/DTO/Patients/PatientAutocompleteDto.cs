namespace Asklepios.Core.DTO.Patients;

public class PatientAutocompleteDto
{
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
}