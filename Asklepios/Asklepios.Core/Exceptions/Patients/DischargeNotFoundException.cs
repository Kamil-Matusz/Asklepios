namespace Asklepios.Core.Exceptions.Patients;

public class DischargeNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public DischargeNotFoundException(Guid id) : base($"Discharge with ID: '{id}' was not found.")
    {
        Id = id;
    }
}