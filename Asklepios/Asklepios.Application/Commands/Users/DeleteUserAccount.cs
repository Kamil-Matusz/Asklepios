using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record DeleteUserAccount(Guid UserId) : ICommand;