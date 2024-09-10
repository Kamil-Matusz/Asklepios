using Asklepios.Application.Services.Clinics;
using Asklepios.Core.DTO.Clinics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Clinics;

public class ClinicPatientsController : BaseController
{
    private readonly IClinicPatientService _clinicPatientService;

    public ClinicPatientsController(IClinicPatientService clinicPatientService)
    {
        _clinicPatientService = clinicPatientService;
    }

    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClinicPatientDto>> GetClinicPatient(Guid id)
        => OkOrNotFound(await _clinicPatientService.GetClinicPatientByIdAsync(id));

    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IReadOnlyList<ClinicPatientDto>>> GetAllClinicPatients([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _clinicPatientService.GetAllClinicPatientsAsync(pageIndex, pageSize));

    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateClinicPatient(ClinicPatientDto dto)
    {
        await _clinicPatientService.AddClinicPatientAsync(dto);
        return CreatedAtAction(nameof(GetClinicPatient), new
        {
            id = dto.ClinicPatientId
        }, null);
    }

    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateClinicPatient(Guid id, ClinicPatientDto dto)
    {
        dto.ClinicPatientId = id;
        await _clinicPatientService.UpdateClinicPatientAsync(dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteClinicPatient(Guid id)
    {
        await _clinicPatientService.DeleteClinicPatientAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpGet("clinicPatientsList")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ClinicPatientDto>>> GetClinicPatientsList()
    {
        var clinicPatients = await _clinicPatientService.GetAllClinicPatientsAsync(0, int.MaxValue); // Adjust paging if needed
        return Ok(clinicPatients);
    }
}