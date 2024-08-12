using Asklepios.Application.Services.Statistics;
using Asklepios.Core.DTO.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Statistics;

public class StatisticsController : BaseController
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("{departmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentStatsDto>> GetDepartmentStats(Guid departmentId)
    {
        var stats = await _statisticsService.GetDepartmentStatsAsync(departmentId);
        if (stats is null)
        {
            return NotFound();
        }
        return Ok(stats);
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("allStats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentStatsDto>> GetAllDepartmentStats()
    {
        var stats = await _statisticsService.GetAllDepartmentStatsAsync();
        return Ok(stats);
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("totalPatients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> GetTotalPatientsCount()
    {
        var totalPatients = await _statisticsService.GetTotalPatientsCountAsync();
        return Ok(totalPatients);
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("totalDepartments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> GetTotalDepartmentsCount()
    {
        var totalDepartments = await _statisticsService.GetTotalDepartmentsCountAsync();
        return Ok(totalDepartments);
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("totalDoctors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> GetTotalDoctorsCount()
    {
        var totalDoctors = await _statisticsService.GetTotalDoctorsCountAsync();
        return Ok(totalDoctors);
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("totalNurses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<int>> GetTotalNursesCount()
    {
        var totalNurses = await _statisticsService.GetTotalNursesCountAsync();
        return Ok(totalNurses);
    }
}