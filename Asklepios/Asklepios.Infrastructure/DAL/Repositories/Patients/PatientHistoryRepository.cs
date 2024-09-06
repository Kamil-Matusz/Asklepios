using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Patients;

public class PatientHistoryRepository : IPatientHistoryRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<PatientHistory> _patientHistories;

    public PatientHistoryRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _patientHistories = _dbContext.PatientHistories;
    }

    public async Task<PatientHistory> GetPatientHistoryByIdAsync(Guid historyId)
    {
        return await _patientHistories
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.HistoryId == historyId);
    }

    public async Task<PatientHistory> GetFullPatientHistoryByPeselAsync(string peselNumber)
    {
        return await _patientHistories
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.PeselNumber == peselNumber);
    }

    public async Task AddPatientHistoryAsync(PatientHistory patientHistory)
    {
        await _patientHistories.AddAsync(patientHistory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePatientHistoryAsync(PatientHistory patientHistory)
    {
        _patientHistories.Update(patientHistory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePatientHistoryAsync(PatientHistory patientHistory)
    {
        _patientHistories.Remove(patientHistory);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> PatientExistAsync(string peselNumber)
    {
        return await _patientHistories
            .AsNoTracking()
            .AnyAsync(x => x.PeselNumber == peselNumber);
    }
}