using Convey.CQRS.Events;

namespace Asklepios.Application.Events;

public record DischargePatient(Guid DischargeId,
    string PatientName,
    string PatientSurname,
    string PeselNumber,
    DateOnly Date,
    string DischargeReasson,
    string Summary,
    Guid MedicalStaffId) : IEvent;