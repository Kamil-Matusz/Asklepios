namespace Asklepios.Core.DTO.Users;

public class NurseDto
{
    public Guid NurseId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Guid UserId { get; set; }
    public Guid DepartmentId { get; set; }
}