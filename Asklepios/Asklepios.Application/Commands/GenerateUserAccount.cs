using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands;

public record GenerateUserAccount(Guid UserId, string Email, string Role, bool IsActive) : ICommand;