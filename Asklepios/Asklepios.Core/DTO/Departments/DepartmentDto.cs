namespace Asklepios.Core.DTO.Departments;

public class DepartmentDto
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int NumberOfBeds { get; set; }
    public int NumberOfPatients { get; set; }
}