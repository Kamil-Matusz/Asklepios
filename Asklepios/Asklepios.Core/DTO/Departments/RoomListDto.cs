namespace Asklepios.Core.DTO.Departments;

public class RoomListDto
{
    public Guid RoomId { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public int NumberOfBeds { get; set; }
    public string DepartmentName { get; set; }
}