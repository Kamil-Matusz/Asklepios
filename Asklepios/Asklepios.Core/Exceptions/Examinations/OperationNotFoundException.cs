namespace Asklepios.Core.Exceptions.Examinations;

public class OperationNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public OperationNotFoundException(Guid id) : base($"Operation with ID: '{id}' was not found.")
    {
        Id = id;
    }
}