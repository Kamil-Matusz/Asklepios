using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Users;

public class MedicalStaffController : BaseController
{
    private readonly IMedicalStaffService _medicalStaffService;

    public MedicalStaffController(IMedicalStaffService medicalStaffService)
    {
        _medicalStaffService = medicalStaffService;
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MedicalStaffListDto>>> GetAllDoctors([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _medicalStaffService.GetAllDoctorsAsync(pageIndex, pageSize));
    
    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicalStaffDto>> GetDoctor(Guid id)
        => OkOrNotFound(await _medicalStaffService.GetDoctorAsync(id));
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateDoctor(MedicalStaffDto dto)
    {
        await _medicalStaffService.AddDoctorAsync(dto);
        return CreatedAtAction(nameof(GetDoctor), new
        {
            id = dto.DoctorId
        }, null);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateDoctor(Guid id, MedicalStaffDto dto)
    {
        dto.DoctorId = id;
        await _medicalStaffService.UpdateDoctorAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDoctor(Guid id)
    {
        await _medicalStaffService.DeleteDoctorAsync(id);
        return NoContent();
    }
}