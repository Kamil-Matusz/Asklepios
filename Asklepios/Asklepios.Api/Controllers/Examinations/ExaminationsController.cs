using System.ComponentModel;
using Asklepios.Application.Services.Examinations;
using Asklepios.Core.DTO.Examinations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Examinations;

public class ExaminationsController : BaseController
{
    private readonly IExaminationService _examinationService;

    public ExaminationsController(IExaminationService examinationService)
    {
        _examinationService = examinationService;
    }
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<ExaminationDto>>> GetAllExaminations([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _examinationService.GetAllExaminationsAsync(pageIndex, pageSize));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet("examinationsByCategory/{category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<ExaminationDto>>> GetAllExaminationsByCategory(string category, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _examinationService.GetAllExaminationsByCategoryAsync(category, pageIndex, pageSize));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExaminationDto>> GetExamination(int id)
        => OkOrNotFound(await _examinationService.GetExaminationAsync(id));
    
    [Authorize(Roles = "Admin, Doctor, Nurse")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateExamination(ExaminationDto dto)
    {
        await _examinationService.AddExaminationAsync(dto);
        return CreatedAtAction(nameof(GetExamination), new
        {
            id = dto.ExamId
        }, null);
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteExamination(int id)
    {
        await _examinationService.DeleteExaminationAsync(id);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateExamination(int id, ExaminationDto dto)
    {
        dto.ExamId = id;
        await _examinationService.UpdateExaminationAsync(dto);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("examinationsList")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ExaminationAutocompleteDto>>> GetExaminationsList()
    {
        var examinations = await _examinationService.GetExaminationsList();
        return Ok(examinations);
    }
}