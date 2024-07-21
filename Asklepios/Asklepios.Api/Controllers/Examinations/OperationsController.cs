using Asklepios.Application.Services.Examinations;
using Asklepios.Core.DTO.Examinations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Examinations;

public class OperationsController : BaseController
{
    private readonly IOperationService _operationService;

    public OperationsController(IOperationService operationService)
    {
        _operationService = operationService;
    }
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OperationItemDto>> GetOperation(Guid id)
        => OkOrNotFound(await _operationService.GetOperationByIdAsync(id));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<OperationItemDto>>> GetAllOperations([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _operationService.GetAllOperationsAsync(pageIndex, pageSize));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet("operationByDoctor/{doctorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<OperationItemDto>>> GetAllOperationsByDoctor(Guid doctorId,[FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _operationService.GetAllOperationsByDoctorIdAsync(doctorId, pageIndex, pageSize));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateOperation(OperationDto dto)
    {
        await _operationService.AddOperationAsync(dto);
        return CreatedAtAction(nameof(GetOperation), new
        {
            id = dto.OperationId
        }, null);
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateOperation(Guid id, OperationDto dto)
    {
        dto.OperationId = id;
        await _operationService.UpdateOperationAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteOperation(Guid id)
    {
        await _operationService.DeleteOperationAsync(id);
        return NoContent();
    }
}