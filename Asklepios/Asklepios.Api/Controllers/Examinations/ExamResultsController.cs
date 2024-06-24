using Asklepios.Application.Services.Examinations;
using Asklepios.Core.DTO.Examinations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Examinations;

public class ExamResultsController : BaseController
{
    private readonly IExamResultService _examResultService;

    public ExamResultsController(IExamResultService examResultService)
    {
        _examResultService = examResultService;
    }
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExamResultDto>> GetExamResult(Guid id)
        => OkOrNotFound(await _examResultService.GetExamResultAsync(id));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<ExamResultListDto>>> GetAllExamResults([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _examResultService.GetAllExamResultsAsync(pageIndex, pageSize));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateExamResult(ExamResultDto dto)
    {
        await _examResultService.AddExamResultAsync(dto);
        return CreatedAtAction(nameof(GetExamResult), new
        {
            id = dto.ExamId
        }, null);
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateDepartment(Guid id, ExamResultDto dto)
    {
        dto.ExamResultId = id;
        await _examResultService.UpdateExamResultAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteExamResult(Guid id)
    {
        await _examResultService.DeleteExamResultAsync(id);
        return NoContent();
    }
}