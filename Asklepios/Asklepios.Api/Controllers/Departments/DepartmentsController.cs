using Asklepios.Application.Services.Departments;
using Asklepios.Core.DTO.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Departments;

public class DepartmentsController : BaseController
{
    private readonly IDeparmentService _departmentService;

    public DepartmentsController(IDeparmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentDetailsDto>> GetDepartment(Guid id)
        => OkOrNotFound(await _departmentService.GetDepartmentAsync(id));

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IReadOnlyList<DepartmentListDto>>> GetAllDepartments([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _departmentService.GetAllDepartmentsAsync(pageIndex, pageSize));

    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateDepartment(DepartmentDto dto)
    {
        await _departmentService.AddDepartmentAsync(dto);
        return CreatedAtAction(nameof(GetDepartment), new
        {
            id = dto.DepartmentId
        }, null);
    }

    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateDepartment(Guid id, DepartmentDetailsDto dto)
    {
        dto.DepartmentId = id;
        await _departmentService.UpdateDepartmentAsync(dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin, IT Employee")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDepartment(Guid id)
    {
        await _departmentService.DeleteDepartmentAsync(id);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("departmentsAutocomplete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<DepartmentAutocompleteDto>>> GetAutocomplete()
    {
        var departments = await _departmentService.GetDepartmentsListAsync();
        return Ok(departments);
    }
}