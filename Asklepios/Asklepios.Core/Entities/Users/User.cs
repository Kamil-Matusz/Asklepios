using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.Entities.Users;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}