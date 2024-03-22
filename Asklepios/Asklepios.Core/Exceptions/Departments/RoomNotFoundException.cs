namespace Asklepios.Core.Exceptions.Departments;

public class RoomNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public RoomNotFoundException(Guid id) : base($"Room with ID: '{id}' was not found.")
    {
        Id = id;
    }
}