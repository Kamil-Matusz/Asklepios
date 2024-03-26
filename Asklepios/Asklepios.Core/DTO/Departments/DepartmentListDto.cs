namespace Asklepios.Core.DTO.Departments;

public class DepartmentListDto
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int NumberOfBeds { get; set; }
    public int ActualNumberOfPatients { get; set; }
}