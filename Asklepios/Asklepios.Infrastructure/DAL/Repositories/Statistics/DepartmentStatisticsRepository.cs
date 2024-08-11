using Asklepios.Core.DTO.Statistics;
using Asklepios.Core.Repositories.Statistics;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Statistics;

public class DepartmentStatisticsRepository : IDepartmentStatisticsRepository
{
    private readonly AsklepiosDbContext _context;

    public DepartmentStatisticsRepository(AsklepiosDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<DepartmentStatsDto>> GetDepartmentStatsAsync(Guid departmentId)
    {
        var departments = await _context.Departments
            .Include(d => d.Patients)
            .Include(d => d.Rooms)
            .Where(x => x.DepartmentId == departmentId)
            .ToListAsync();

        var departmentStatsDto = new DepartmentStatsDto
        {
            TotalDepartments = departments.Count,
            DepartmentDetails = departments.ToDictionary(
                department => department.DepartmentName,
                department => new DepartmentDetailDto
                {
                    DepartmentName = department.DepartmentName,
                    TotalBeds = department.NumberOfBeds,
                    PatientsCount = department.Patients.Count(),
                    TotalRooms = department.Rooms.Count()
                })
        };
        
        return new List<DepartmentStatsDto> { departmentStatsDto };
    }

    public async Task<DepartmentStatsDto> GetAllDepartmentStatsAsync()
    {
        var departments = await _context.Departments
            .Include(d => d.Patients)
            .Include(d => d.Rooms)
            .ToListAsync();

        var departmentStatsDto = new DepartmentStatsDto
        {
            TotalDepartments = departments.Count,
            DepartmentDetails = departments.ToDictionary(
                department => department.DepartmentName,
                department => new DepartmentDetailDto
                {
                    DepartmentName = department.DepartmentName,
                    TotalBeds = department.NumberOfBeds,
                    PatientsCount = department.Patients.Count(),
                    TotalRooms = department.Rooms.Count()
                })
        };

        return departmentStatsDto;
    }
    
    public async Task<int> GetTotalPatientsCountAsync()
    {
        return await _context.Patients.CountAsync(p => !p.IsDischarged);
    }

    public async Task<int> GetTotalDepartmentsCountAsync()
    {
        return await _context.Departments.CountAsync();
    }
}