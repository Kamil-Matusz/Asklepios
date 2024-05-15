using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Discharges;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Commands.Handlers.Discharges;

public class AddDischargeHandler : ICommandHandler<AddDischarge>
{
    private readonly IDischargeRepository _dischargeRepository;

    public AddDischargeHandler(IDischargeRepository dischargeRepository)
    {
        _dischargeRepository = dischargeRepository;
    }

    public async Task HandlerAsync(AddDischarge command)
    {
        var discharge = new Discharge(command.DischargeId, command.PatientName, command.PatientSurname, command.PeselNumber,command.Address,
            command.Date, command.DischargeReasson, command.Summary, command.MedicalStaffId);
        
        await _dischargeRepository.AddDischargeAsync(discharge);
    }
}