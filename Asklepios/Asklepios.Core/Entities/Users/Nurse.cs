using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Entities.Users;

public class Nurse
{
    public Guid NurseId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public User User { get; set; }
    public Department Department { get; set; }
}