namespace Asklepios.Core.DTO.Users;

public class MedicalStaffListDto
{
    public Guid DoctorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PrivatePhoneNumber { get; set; }
    public string HospitalPhoneNumber { get; set; }
    public string Specialization { get; set; }
    public string MedicalLicenseNumber { get; set; }
    public string DepartmentName { get; set; }
}