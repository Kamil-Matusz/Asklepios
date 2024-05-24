using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record GenerateUserAccount(Guid UserId, string Email, string Role, bool IsActive) : ICommand;