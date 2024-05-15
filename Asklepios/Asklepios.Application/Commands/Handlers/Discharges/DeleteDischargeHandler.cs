using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Discharges;
using Asklepios.Core.Exceptions.Patients;
using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Commands.Handlers.Discharges;

public class DeleteDischargeHandler : ICommandHandler<DeleteDischarge>
{
    private readonly IDischargeRepository _dischargeRepository;

    public DeleteDischargeHandler(IDischargeRepository dischargeRepository)
    {
        _dischargeRepository = dischargeRepository;
    }

    public async Task HandlerAsync(DeleteDischarge command)
    {
        var discharge = await _dischargeRepository.GetDischargeByIdAsync(command.DischargeId);
        if (discharge is null)
        {
            throw new DischargeNotFoundException(command.DischargeId);
        }

        await _dischargeRepository.DeleteDischargeAsync(discharge);
    }
}