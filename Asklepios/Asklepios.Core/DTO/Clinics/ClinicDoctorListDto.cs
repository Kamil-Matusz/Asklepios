namespace Asklepios.Core.DTO.Clinics;

public class ClinicDoctorListDto
{
    public Guid DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Specialization { get; set; }
}