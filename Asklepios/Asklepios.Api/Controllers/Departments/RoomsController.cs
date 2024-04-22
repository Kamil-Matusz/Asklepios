using Asklepios.Application.Services.Departments;
using Asklepios.Core.DTO.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Departments;

[Authorize]
public class RoomsController : BaseController
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomDto>> GetRoom(Guid id)
        => OkOrNotFound(await _roomService.GetRoomAsync(id));

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomListDto>>> GetAllRooms([FromQuery] int pageIndex, [FromQuery] int pageSize)
        => Ok(await _roomService.GetAllRoomsAsync(pageIndex, pageSize));

    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateRoom(RoomDto dto)
    {
        await _roomService.AddRoomAsync(dto);
        return CreatedAtAction(nameof(GetRoom), new
        {
            id = dto.RoomId
        }, null);
    }

    [Authorize(Roles = "Admin, IT Employee")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateRoom(Guid id, RoomDto dto)
    {
        dto.RoomId = id;
        await _roomService.UpdateRoomAsync(dto);
        return NoContent();
    }

    [Authorize(Roles = "Admin, IT Employee")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRoom(Guid id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }
}