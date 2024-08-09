using Asklepios.Application.Services.Statistics;
using Asklepios.Core.DTO.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Statistics;

public class DepartmentStatisticsController : BaseController
{
    private readonly IDepartmentStatisticsService _departmentStatisticsService;

    public DepartmentStatisticsController(IDepartmentStatisticsService departmentStatisticsService)
    {
        _departmentStatisticsService = departmentStatisticsService;
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpGet("{departmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentStatsDto>> GetDepartmentStats(Guid departmentId)
    {
        var stats = await _departmentStatisticsService.GetDepartmentStatsAsync(departmentId);
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
        var stats = await _departmentStatisticsService.GetAllDepartmentStatsAsync();
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
        var totalPatients = await _departmentStatisticsService.GetTotalPatientsCountAsync();
        return Ok(totalPatients);
    }
}