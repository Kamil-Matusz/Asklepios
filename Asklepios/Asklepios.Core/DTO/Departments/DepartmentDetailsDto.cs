using Asklepios.Core.DTO.Patients;

namespace Asklepios.Core.DTO.Departments;

public class DepartmentDetailsDto : DepartmentDto
{
    public List<RoomDto> Rooms { get; set; }
    public List<PatientDto> Patients { get; set; }
}