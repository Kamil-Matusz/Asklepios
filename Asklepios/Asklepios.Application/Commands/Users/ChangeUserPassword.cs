using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record ChangeUserPassword(Guid UserId, string Password) : ICommand;