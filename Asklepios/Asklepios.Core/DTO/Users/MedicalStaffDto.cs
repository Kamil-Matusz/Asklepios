namespace Asklepios.Core.DTO.Users;

public class MedicalStaffDto
{
    public Guid DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PrivatePhoneNumber { get; set; }
    public string HospitalPhoneNumber { get; set; }
    public string Specialization { get; set; }
    public string MedicalLicenseNumber { get; set; }
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
}