using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Users;

public class NurseController : BaseController
{
    private readonly INurseService _nurseService;

    public NurseController(INurseService nurseService)
    {
        _nurseService = nurseService;
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<NurseListDto>>> GetAllNurses([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _nurseService.GetAllNursesAsync(pageIndex, pageSize));
    
    [Authorize]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NurseDto>> GetNurse(Guid id)
        => OkOrNotFound(await _nurseService.GetNurseAsync(id));
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateNurse(NurseDto dto)
    {
        await _nurseService.AddNurseAsync(dto);
        return CreatedAtAction(nameof(GetNurse), new
        {
            id = dto.NurseId
        }, null);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateNurse(Guid id, NurseDto dto)
    {
        dto.NurseId = id;
        await _nurseService.UpdateNurseAsync(dto);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin, IT Employee")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteNurse(Guid id)
    {
        await _nurseService.DeleteNurseAsync(id);
        return NoContent();
    }
}