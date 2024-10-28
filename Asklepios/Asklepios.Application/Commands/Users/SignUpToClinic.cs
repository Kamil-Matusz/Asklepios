using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record SignUpToClinic(Guid UserId, string Email, string Password) : ICommand;