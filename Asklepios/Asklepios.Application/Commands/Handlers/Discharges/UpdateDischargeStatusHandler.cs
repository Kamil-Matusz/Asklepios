using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Discharges;
using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Commands.Handlers.Discharges;

public class UpdateDischargeStatusHandler : ICommandHandler<UpdateDischargeStatus>
{
    private readonly IPatientRepository _patientRepository;

    public UpdateDischargeStatusHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task HandlerAsync(UpdateDischargeStatus command)
    {
        var patient = await _patientRepository.GetPatientDataByIdAsync(command.PatientId);

        patient.IsDischarged = command.IsDischarged;

        await _patientRepository.UpdatePatientAsync(patient);
    }
}