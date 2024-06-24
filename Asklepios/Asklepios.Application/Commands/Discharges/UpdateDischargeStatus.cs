using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Discharges;

public record UpdateDischargeStatus(Guid PatientId, bool IsDischarged) : ICommand;