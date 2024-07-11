using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record ChangeAccountStatus(Guid UserId, bool status) : ICommand;