namespace Asklepios.Core.Exceptions.Departments;

public class CannotDeleteDepartmentException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotDeleteDepartmentException(Guid id) : base($"Department with ID: '{id}' cannot be deleted.")
    {
        Id = id;
    }
}