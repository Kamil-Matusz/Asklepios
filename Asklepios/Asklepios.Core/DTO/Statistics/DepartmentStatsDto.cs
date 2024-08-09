namespace Asklepios.Core.DTO.Statistics;

public class DepartmentStatsDto
{
    public int TotalDepartments { get; set; }
    public Dictionary<string, DepartmentDetailDto> DepartmentDetails { get; set; }

    public DepartmentStatsDto()
    {
        DepartmentDetails = new Dictionary<string, DepartmentDetailDto>();
    }
}

public class DepartmentDetailDto
{
    public string DepartmentName { get; set; }
    public int TotalBeds { get; set; }
    public int PatientsCount { get; set; }
    public int TotalRooms { get; set; }
}
