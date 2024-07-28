using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Repositories.Patients;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Patients;

public class PatientRepository : IPatientRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Patient> _patients;

    public PatientRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _patients = _dbContext.Patients;
    }
    
    public async Task<Patient> GetPatientByIdAsync(Guid patientId)
        => await _patients
            .Include(x => x.Department)
            .Include(x => x.Room)
            .Include(x => x.Operations)
            .Include(x => x.ExamResults)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.PatientId == patientId);

    public async Task<Patient> GetPatientDataByIdAsync(Guid patientId)
        => await _patients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.PatientId == patientId);

    public async Task<IReadOnlyList<Patient>> GetAllPatientsAsync(int pageIndex, int pageSize)
        => await _patients
            .Include(x => x.Department)
            .Include(x => x.Room)
            .Include(x => x.Operations)
            .Include(x => x.ExamResults)
            .AsNoTracking()
            .OrderBy(x => x.PatientSurname)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<IReadOnlyList<Patient>> GetAllPatientsByDepartmentAsync(Guid departmentId, int pageIndex, int pageSize)
        => await _patients
            .Include(x => x.Department)
            .Include(x => x.Room)
            .Include(x => x.Operations)
            .Include(x => x.ExamResults)
            .Where(x => x.DepartmentId == departmentId)
            .AsNoTracking()
            .OrderBy(x => x.PatientSurname)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<IReadOnlyList<Patient>> GetAllPatientsByRoomAsync(Guid roomId, int pageIndex, int pageSize)
        => await _patients
            .Include(x => x.Department)
            .Include(x => x.Room)
            .Include(x => x.Operations)
            .Include(x => x.ExamResults)
            .Where(x => x.RoomId == roomId)
            .AsNoTracking()
            .OrderBy(x => x.PatientSurname)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddPatientAsync(Patient patient)
    {
        await _patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePatientAsync(Patient patient)
    {
        _patients.Update(patient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeletePatientAsync(Patient patient)
    {
        _patients.Remove(patient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Patient>> GetPatientsList()
        => await _patients
            .AsNoTracking()
            .OrderBy(x => x.PatientSurname)
            .Where(x => x.IsDischarged == false)
            .ToListAsync();
}