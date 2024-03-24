using Asklepios.Application.Services.Departments;
using Asklepios.Core.DTO.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Departments;

public class DepartmentsController : BaseController
{
    private readonly IDeparmentService _departmentService;

    public DepartmentsController(IDeparmentService departmentService)
    {
        _departmentService = departmentService;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DepartmentDetailsDto>> GetDepartment(Guid id)
        => OkOrNotFound(await _departmentService.GetDepartmentAsync(id));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetAllDepartments()
        => Ok(await _departmentService.GetAllDepartmentsAsync());

    [HttpPost]
    public async Task<ActionResult> CreateDepartment(DepartmentDto dto)
    {
        await _departmentService.AddDepartmentAsync(dto);
        return CreatedAtAction(nameof(GetDepartment), new
        {
            id = dto.DepartmentId
        }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateDepartment(Guid id, DepartmentDetailsDto dto)
    {
        dto.DepartmentId = id;
        await _departmentService.UpdateDepartmentAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteDepartment(Guid id)
    {
        await _departmentService.DeleteDepartmentAsync(id);
        return NoContent();
    }
}