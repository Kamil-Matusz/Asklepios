using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries.Discharges;
using Asklepios.Core.DTO.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

public class GetDoctorDischargesHandler : IQueryHandler<GetDoctorDischarges, IEnumerable<DischargeItemDto>>
{
    private readonly AsklepiosDbContext _dbContext;

    public GetDoctorDischargesHandler(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DischargeItemDto>> HandlerAsync(GetDoctorDischarges query)
    {
        var doctorId = query.DoctorId;
        var discharges = await _dbContext.Discharges
            .Include(x => x.MedicalStaff)
            .AsNoTracking()
            .Where(x => x.MedicalStaffId == query.DoctorId)
            .ToListAsync();

        return discharges.Select(d => d.AsDischargeItemDto()).ToList();
    }
}