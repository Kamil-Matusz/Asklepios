using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record ChangeUserRole(Guid UserId, string Role) : ICommand;