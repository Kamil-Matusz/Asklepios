using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.DTO.Users;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Users;

public class NurseController : BaseController
{
    private readonly INurseService _nurseService;

    public NurseController(INurseService nurseService)
    {
        _nurseService = nurseService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<NurseListDto>>> GetAllNurses([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _nurseService.GetAllNursesAsync(pageIndex, pageSize));
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<NurseDto>> GetNurse(Guid id)
        => OkOrNotFound(await _nurseService.GetNurseAsync(id));
    
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
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateNurse(Guid id, NurseDto dto)
    {
        dto.NurseId = id;
        await _nurseService.UpdateNurseAsync(dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteNurse(Guid id)
    {
        await _nurseService.DeleteNurseAsync(id);
        return NoContent();
    }
}