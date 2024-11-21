using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Repositories.Clinics;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Clinics;

public class ClinicAppointmentRepository : IClinicAppointmentRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<ClinicAppointment> _clinicAppointments;

    public ClinicAppointmentRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _clinicAppointments = _dbContext.Set<ClinicAppointment>();
    }
    
    public async Task<ClinicAppointment> GetAppointmentByIdAsync(Guid appointmentId)
            => await _clinicAppointments
                .Include(x => x.ClinicPatient)
                .Include(x => x.MedicalStaff)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);

        public async Task<IReadOnlyList<ClinicAppointment>> GetAppointmentsByPatientIdAsync(Guid clinicPatientId, int pageIndex, int pageSize)
            => await _clinicAppointments
                .Where(x => x.ClinicPatientId == clinicPatientId)
                .Include(x => x.ClinicPatient)
                .Include(x => x.MedicalStaff)
                .AsNoTracking()
                .OrderBy(x => x.AppointmentDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<IReadOnlyList<ClinicAppointment>> GetAppointmentsByMedicalStaffIdAsync(Guid medicalStaffId, int pageIndex, int pageSize)
            => await _clinicAppointments
                .Where(x => x.MedicalStaffId == medicalStaffId)
                .Include(x => x.ClinicPatient)
                .Include(x => x.MedicalStaff)
                .AsNoTracking()
                .OrderBy(x => x.AppointmentDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
        public async Task<IReadOnlyList<ClinicAppointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            return await _clinicAppointments
                .Where(x => x.AppointmentDate.Date == date.Date)
                .Include(x => x.ClinicPatient)
                .Include(x => x.MedicalStaff)
                .AsNoTracking()
                .OrderBy(x => x.AppointmentDate)
                .ToListAsync();
        }

        public async Task AddAppointmentAsync(ClinicAppointment appointment)
        {
            await _clinicAppointments.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(ClinicAppointment appointment)
        {
            _clinicAppointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(ClinicAppointment appointment)
        {
            _clinicAppointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ClinicAppointment>> GetUserPastAppointmentsAsync(Guid clinicPatientId)
        {
            var date = DateTime.Now;
            return await _clinicAppointments
                .Where(x => x.AppointmentDate.Date < date && x.ClinicPatientId == clinicPatientId)
                .Include(x => x.ClinicPatient)
                .Include(x => x.MedicalStaff)
                .AsNoTracking()
                .OrderBy(x => x.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ClinicAppointment>> GetUserFutureAppointmentsAsync(Guid clinicPatientId)
        {
            var date = DateTime.Now;
            return await _clinicAppointments
                .Where(x => x.AppointmentDate.Date > date && x.ClinicPatientId == clinicPatientId)
                .Include(x => x.ClinicPatient)
                .Include(x => x.MedicalStaff)
                .AsNoTracking()
                .OrderBy(x => x.AppointmentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClinicAppointment>> GetAppointmentsOlderThanAsync(DateOnly date)
            => await _clinicAppointments
                .Where(d => DateOnly.FromDateTime(d.AppointmentDate) < date)
                .ToListAsync();

        public async Task DeleteAppointmentByIdAsync(Guid appointmentId)
        {
            var appointment = await _clinicAppointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _clinicAppointments.Remove(appointment);
                await _dbContext.SaveChangesAsync();
            }
        }
}