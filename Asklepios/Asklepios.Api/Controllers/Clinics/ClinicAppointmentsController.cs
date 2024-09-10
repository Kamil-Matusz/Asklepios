using Asklepios.Application.Services.Clinics;
using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.DTO.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Clinics;

public class ClinicAppointmentsController : BaseController
{
    private readonly IClinicAppointmentService _clinicAppointmentService;

    public ClinicAppointmentsController(IClinicAppointmentService clinicAppointmentService)
    {
        _clinicAppointmentService = clinicAppointmentService;
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
}