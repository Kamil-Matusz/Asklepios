using Asklepios.Application.Events;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Convey.CQRS.Events;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.Events.Handlers;

public class UpdateDischargeStatusHandler : IEventHandler<UpdateDischargeStatus>
{
    private readonly AsklepiosDbContext _asklepiosDbContext;

    public UpdateDischargeStatusHandler(AsklepiosDbContext asklepiosDbContext)
    {
        _asklepiosDbContext = asklepiosDbContext;
    }

    public async Task HandleAsync(UpdateDischargeStatus @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var patient = await _asklepiosDbContext.Patients
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.PatientId == @event.PatientId);

        patient.IsDischarged = @event.DischargeStatus;

        _asklepiosDbContext.Patients.Update(patient);
        await _asklepiosDbContext.SaveChangesAsync();
    }
}