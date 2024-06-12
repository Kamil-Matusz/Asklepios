using Asklepios.Core.Entities.Patients;

namespace Asklepios.Core.Entities.Departments;

public class Room
{
    public Guid RoomId { get; set; }
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public int NumberOfBeds { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    public IEnumerable<Patient> Patients { get; set; }

    public Room(Guid roomId, int roomNumber, string roomType, int numberOfBeds, Guid departmentId)
    {
        RoomId = roomId;
        RoomNumber = roomNumber;
        RoomType = roomType;
        NumberOfBeds = numberOfBeds;
        DepartmentId = departmentId;
    }

    public Room()
    {
    }
}