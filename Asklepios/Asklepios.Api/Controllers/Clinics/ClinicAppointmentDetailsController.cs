using Asklepios.Application.Services.Clinics;
using Asklepios.Core.DTO.Clinics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Clinics;

public class ClinicAppointmentDetailsController : BaseController
{
    private readonly IClinicAppointmentDetailsService _clinicAppointmentDetailsService;

    public ClinicAppointmentDetailsController(IClinicAppointmentDetailsService clinicAppointmentDetailsService)
    {
        _clinicAppointmentDetailsService = clinicAppointmentDetailsService;
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateAppointmentDetails(ClinicAppointmentDetailsDto dto)
    {
        await _clinicAppointmentDetailsService.AddClinicAppointmentDetailsAsync(dto);
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{appointmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteClinicAppointmentDetails(Guid appointmentId)
    {
        await _clinicAppointmentDetailsService.DeleteClinicAppointmentDetailsByAppointmentIdAsync(appointmentId);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{appointmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateClinicAppointmentDetails(Guid appointmentId, ClinicAppointmentDetailsDto dto)
    {
        dto.AppointmentId = appointmentId;
        await _clinicAppointmentDetailsService.UpdateClinicPatientAsync(dto);
        return Ok();
    }
    
    [Authorize]
    [HttpGet("{appointmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClinicAppointmentDetailsDto>> GetClinicAppointmentDetailsById(Guid appointmentId)
        => OkOrNotFound(await _clinicAppointmentDetailsService.GetClinicAppointmentDetailsByAppointmentIdAsync(appointmentId));
}