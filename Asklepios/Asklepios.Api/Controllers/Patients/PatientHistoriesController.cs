using Asklepios.Application.Services.Patients;
using Asklepios.Core.DTO.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Patients;

public class PatientHistoriesController : BaseController
{
    private readonly IPatientHistoryService _patientHistoryService;

    public PatientHistoriesController(IPatientHistoryService patientHistoryService)
    {
        _patientHistoryService = patientHistoryService;
    }
    
    [Authorize(Roles = "Doctor, Admin, Nurse, IT Employee")]
    [HttpGet("{peselNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatientHistoryDto>> GetPatientHistoriesByPesel(string peselNumber)
    {
        var patientHistory = await _patientHistoryService.GetFullPatientHistoryByPeselAsync(peselNumber);
        
        if (patientHistory is null)
        {
            return NotFound();
        }

        return Ok(patientHistory);
    }
    
    [Authorize(Roles = "Admin, Doctor, IT Employee")]
    [HttpDelete("{historyId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeletePatientHistory(Guid historyId)
    {
        await _patientHistoryService.DeletePatientHistoryAsync(historyId);
        return NoContent();
    }
}