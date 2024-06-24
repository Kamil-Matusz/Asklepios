using Convey.CQRS.Events;

namespace Asklepios.Application.Events;

public record UpdateDischargeStatus(Guid PatientId, bool DischargeStatus) : IEvent;