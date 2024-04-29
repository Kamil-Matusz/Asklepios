using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands;

public record SignUp(Guid UserId, string Email, string Password, string Role, bool IsActive) : ICommand;