using Asklepios.Application.Services.Clinics;
using Asklepios.Core.DTO.Clinics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Clinics;

public class ClinicAppointmentsController : BaseController
{
    private readonly IClinicAppointmentService _clinicAppointmentService;
    private readonly IClinicPatientService _clinicPatientService;

    public ClinicAppointmentsController(IClinicAppointmentService clinicAppointmentService, IClinicPatientService clinicPatientService)
    {
        _clinicAppointmentService = clinicAppointmentService;
        _clinicPatientService = clinicPatientService;
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePatientAppointment(ClinicAppointmentRequestDto dto)
    {
        await _clinicAppointmentService.RegisterPatientAndCreateAppointmentAsync(dto);
        return Ok();
    }
    
    [Authorize]
    [HttpPost("admissionByUser")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePatientAppointmentByUser(ClinicAppointmentRequestByUserDto dto)
    {
        dto.UserId = Guid.Parse(User.Identity?.Name);
        await _clinicAppointmentService.RegisterPatientAndCreateAppointmentBuUserAsync(dto);
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteClinicAppointment(Guid id)
    {
        await _clinicAppointmentService.DeleteClinicAppointmentAsync(id);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateClinicAppointmentStatus(Guid id, ClinicAppointmentStatusDto dto)
    {
        dto.AppointmentId = id;
        await _clinicAppointmentService.UpdateClinicAppointmentAsync(dto);
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClinicAppointmentListDto>> GetClinicPatient(Guid id)
        => OkOrNotFound(await _clinicAppointmentService.GetClinicAppointmentByIdAsync(id));
    
    [Authorize(Roles = "Admin, Nurse, Doctor")]
    [HttpGet("todayAddmissions/{date:datetime}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ClinicAppointmentListDto>>> GetClinicAppointmentsByDate(DateTime date)
    {
        var appointments = await _clinicAppointmentService.GetClinicAppointmentsByDateAsync(date);
        return Ok(appointments);
    }
    
    [Authorize]
    [HttpGet("userPastClinicAppointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ClinicAppointmentListDto>>> GetUserPastClinicAppointments()
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var clinicPatientId = await _clinicPatientService.GetClinicPatientIdAsync(userId);
        var appointments = await _clinicAppointmentService.GetUserPastClinicAppointments(clinicPatientId);
        return Ok(appointments);
    }
    
    [Authorize]
    [HttpGet("userFutureClinicAppointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<ClinicAppointmentListDto>>> GetUserFutureClinicAppointments()
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var clinicPatientId = await _clinicPatientService.GetClinicPatientIdAsync(userId);
        var appointments = await _clinicAppointmentService.GetUserFutureClinicAppointments(clinicPatientId);
        return Ok(appointments);
    }

}