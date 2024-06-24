namespace Asklepios.Core.DTO.Departments;

public class RoomDto
{
    public Guid RoomId { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public int NumberOfBeds { get; set; }
    public Guid DepartmentId { get; set; }
}