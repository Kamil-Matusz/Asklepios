using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Discharges;

public record DeleteDischarge(Guid DischargeId) : ICommand;