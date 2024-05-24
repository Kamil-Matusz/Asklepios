using Asklepios.Core.Entities.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Users;

public class MedicalStaffRepository : IMedicalStaffRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<MedicalStaff> _medicalStaves;

    public MedicalStaffRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _medicalStaves = _dbContext.MedicalStaff;
    }
    
    public async Task<MedicalStaff> GetDoctorByIdAsync(Guid doctorId)
        => await _medicalStaves
            .AsNoTracking()
            .Include(x => x.Department)
            .SingleOrDefaultAsync(x => x.DoctorId == doctorId);

    public async Task<IReadOnlyList<MedicalStaff>> GetAllNDoctorsAsync(int pageIndex, int pageSize)
        => await _medicalStaves
            .AsNoTracking()
            .Include(x => x.Department)
            .OrderBy(x => x.Surname)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddDoctorAsync(MedicalStaff medicalStaff)
    {
        await _medicalStaves.AddAsync(medicalStaff);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDoctorAsync(MedicalStaff medicalStaff)
    {
        _medicalStaves.Update(medicalStaff);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteDoctorAsync(MedicalStaff medicalStaff)
    {
        _medicalStaves.Remove(medicalStaff);
        await _dbContext.SaveChangesAsync();
    }
}