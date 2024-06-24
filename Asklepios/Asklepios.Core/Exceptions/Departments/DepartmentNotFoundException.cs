namespace Asklepios.Core.Exceptions.Departments;

public class DepartmentNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public DepartmentNotFoundException(Guid id) : base($"Department with ID: '{id}' was not found.")
    {
        Id = id;
    }
}