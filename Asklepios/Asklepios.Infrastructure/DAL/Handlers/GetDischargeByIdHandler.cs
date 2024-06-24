using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries.Discharges;
using Asklepios.Core.DTO.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

public class GetDischargeByIdHandler : IQueryHandler<GetDischargeById, DischargeItemDto>
{
    private readonly AsklepiosDbContext _dbContext;

    public GetDischargeByIdHandler(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DischargeItemDto> HandlerAsync(GetDischargeById query)
    {
        var dischargeId = query.DischargeId;
        var discharge = await _dbContext.Discharges
            .Include(x => x.MedicalStaff)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.DischargeId == dischargeId);

        return discharge.AsDischargeItemDto();
    }
}