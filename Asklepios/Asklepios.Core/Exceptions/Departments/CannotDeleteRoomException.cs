namespace Asklepios.Core.Exceptions.Departments;

public class CannotDeleteRoomException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotDeleteRoomException(Guid id) : base($"Room with ID: '{id}' cannot be deleted.")
    {
        Id = id;
    }
}