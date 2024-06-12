using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Discharges;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Commands.Handlers.Discharges;

public class UpdateDischargeHandler : ICommandHandler<UpdateDischarge>
{
    private readonly IDischargeRepository _dischargeRepository;

    public UpdateDischargeHandler(IDischargeRepository dischargeRepository)
    {
        _dischargeRepository = dischargeRepository;
    }

    public async Task HandlerAsync(UpdateDischarge command)
    {
        var discharge = new Discharge(command.DischargeId, command.PatientName, command.PatientSurname, command.PeselNumber,command.Address,
            command.Date, command.DischargeReasson, command.Summary, command.MedicalStaffId);
        
        await _dischargeRepository.UpdateDischargeAsync(discharge);
    }
}