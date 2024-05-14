using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories;

public class DischargeRepository : IDischargeRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Discharge> _discharges;

    public DischargeRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _discharges = _dbContext.Discharges;
    }
    
    public async Task<Discharge> GetDischargeByIdAsync(Guid dischargeId)
        => await _discharges
            .Include(x => x.MedicalStaff)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.DischargeId == dischargeId);

    public async Task AddDischargeAsync(Discharge discharge)
    {
        await _discharges.AddAsync(discharge);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDischargeAsync(Discharge discharge)
    {
        _discharges.Update(discharge);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteDischargeAsync(Discharge discharge)
    {
        _discharges.Remove(discharge);
        await _dbContext.SaveChangesAsync();
    }
}