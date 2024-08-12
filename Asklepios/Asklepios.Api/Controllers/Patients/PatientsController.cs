using Asklepios.Application.Services.Patients;
using Asklepios.Core.DTO.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Patients;

public class PatientsController : BaseController
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatientDetailsDto>> GetPatient(Guid id)
        => OkOrNotFound(await _patientService.GetPatientAsync(id));
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<PatientListDto>>> GetAllPatients([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _patientService.GetAllPatientsAsync(pageIndex, pageSize));
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePatient(PatientDto dto)
    {
        await _patientService.AddPatientAsync(dto);
        return CreatedAtAction(nameof(GetPatient), new
        {
            id = dto.PatientId
        }, null);
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdatePatient(Guid id, PatientDto dto)
    {
        dto.PatientId = id;
        await _patientService.UpdatePatientAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePatient(Guid id)
    {
        await _patientService.DeletePatientAsync(id);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("patientsList")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<PatientAutocompleteDto>>> GetPatientsList()
    {
        var patients = await _patientService.GetPatientsList();
        return Ok(patients);
    }
}