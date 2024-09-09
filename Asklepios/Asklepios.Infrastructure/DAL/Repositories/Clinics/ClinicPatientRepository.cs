using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Repositories.Clinics;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Clinics;

public class ClinicPatientRepository : IClinicPatientRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<ClinicPatient> _clinicPatients;

    public ClinicPatientRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _clinicPatients = _dbContext.ClinicPatients;
    }
    
    public async Task<ClinicPatient> GetClinicPatientByIdAsync(Guid clinicPatientId)
        => await _clinicPatients
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ClinicPatientId == clinicPatientId);

    public async Task<IReadOnlyList<ClinicPatient>> GetAllClinicPatientsAsync(int pageIndex, int pageSize)
        => await _clinicPatients
            .AsNoTracking()
            .OrderBy(x => x.PatientSurname)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    

    public async Task AddClinicPatientAsync(ClinicPatient clinicPatient)
    {
        await _clinicPatients.AddAsync(clinicPatient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateClinicPatientAsync(ClinicPatient clinicPatient)
    {
        _clinicPatients.Update(clinicPatient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteClinicPatientAsync(ClinicPatient clinicPatient)
    {
        _clinicPatients.Remove(clinicPatient);
        await _dbContext.SaveChangesAsync();
    }
    
}