using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries.Discharges;
using Asklepios.Core.DTO.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

public class GetAllDischargesHandler : IQueryHandler<GetAllDischarges, IEnumerable<DischargeItemDto>>
{
    private readonly AsklepiosDbContext _dbContext;

    public GetAllDischargesHandler(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<DischargeItemDto>> HandlerAsync(GetAllDischarges query)
    {
        var discharges = await _dbContext.Discharges
            .Include(x => x.MedicalStaff)
            .AsNoTracking()
            .ToListAsync();

        return discharges.Select(d => d.AsDischargeItemDto()).ToList();
    }
}