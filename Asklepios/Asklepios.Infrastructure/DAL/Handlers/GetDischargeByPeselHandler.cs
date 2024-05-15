using Asklepios.Application.Abstractions;
using Asklepios.Application.Queries.Discharges;
using Asklepios.Core.DTO.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Handlers;

public class GetDischargeByPeselHandler : IQueryHandler<GetDischargeByPesel, DischargeItemDto>
{
    private readonly AsklepiosDbContext _dbContext;

    public GetDischargeByPeselHandler(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DischargeItemDto> HandlerAsync(GetDischargeByPesel query)
    {
        var peselNumber = query.PeselNumber;
        var discharge = await _dbContext.Discharges
            .Include(x => x.MedicalStaff)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.PeselNumber == peselNumber);

        return discharge.AsDischargeItemDto();
    }
}