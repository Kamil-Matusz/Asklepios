using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands;

public record DeleteUserAccount(Guid UserId) : ICommand;