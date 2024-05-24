using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Users;

public record SignIn(string Email, string Password) : ICommand;