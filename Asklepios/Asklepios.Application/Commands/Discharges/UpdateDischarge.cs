using Asklepios.Application.Abstractions;

namespace Asklepios.Application.Commands.Discharges;

public record UpdateDischarge(Guid DischargeId, string PatientName, string PatientSurname, string PeselNumber, 
    string Address, DateOnly Date, string DischargeReasson, string Summary, Guid MedicalStaffId) : ICommand;