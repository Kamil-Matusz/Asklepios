namespace Asklepios.Core.Exceptions.Patients;

public class CannotAddPatientToRoomException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotAddPatientToRoomException(Guid id) : base("The patient cannot be added to the unit. No seats available.")
    {
        Id = id;
    }
}