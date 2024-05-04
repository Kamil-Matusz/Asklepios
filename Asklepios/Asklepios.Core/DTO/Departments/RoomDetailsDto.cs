using Asklepios.Core.DTO.Patients;

namespace Asklepios.Core.DTO.Departments;

public class RoomDetailsDto : RoomDto
{
    public List<PatientDto> Patients { get; set; }
}