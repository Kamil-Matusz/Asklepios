namespace Asklepios.Core.DTO.Departments;

public class DepartmentDetailsDto : DepartmentDto
{
    public List<RoomDto> Rooms { get; set; }
}