using Asklepios.Application.Events;
using Asklepios.Core.Entities.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Convey.CQRS.Events;

namespace Asklepios.Infrastructure.Events.Handlers;

public class DischargePersonEventHandler : IEventHandler<DischargePatient>
{
    private readonly AsklepiosDbContext _asklepiosDbContext;

    public DischargePersonEventHandler(AsklepiosDbContext asklepiosDbContext)
    {
        _asklepiosDbContext = asklepiosDbContext;
    }

    public async Task HandleAsync(DischargePatient @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var discharge = new Discharge
        {
            DischargeId = @event.DischargeId,
            PatientName = @event.PatientName,
            PatientSurname = @event.PatientSurname,
            PeselNumber = @event.PeselNumber,
            Date = @event.Date,
            DischargeReasson = @event.DischargeReasson,
            Summary = @event.Summary,
            MedicalStaffId = @event.MedicalStaffId
        };

        _asklepiosDbContext.Discharges.Add(discharge);
        await _asklepiosDbContext.SaveChangesAsync();
    }
}