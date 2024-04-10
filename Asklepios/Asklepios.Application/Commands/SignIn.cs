using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;