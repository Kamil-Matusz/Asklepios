using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Entities.Users;

public class Nurse
{
    public Guid NurseId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public Guid UserId { get; set; } 
    public User User { get; set; }
    
    public Guid DepartmentId { get; set; } 
    public Department Department { get; set; }
}