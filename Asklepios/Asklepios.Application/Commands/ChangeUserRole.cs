using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands;

public record ChangeUserRole(Guid UserId, string Role) : ICommand;