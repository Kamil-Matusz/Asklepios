using Asklepios.Application.Services.Departments;
using Asklepios.Core.DTO.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Departments;

public class RoomsController : BaseController
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoomDto>> GetRoom(Guid id)
        => OkOrNotFound(await _roomService.GetRoomAsync(id));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RoomListDto>>> GetAllRooms()
        => Ok(await _roomService.GetAllRoomsAsync());

    [HttpPost]
    public async Task<ActionResult> CreateRoom(RoomDto dto)
    {
        await _roomService.AddRoomAsync(dto);
        return CreatedAtAction(nameof(GetRoom), new
        {
            id = dto.RoomId
        }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateRoom(Guid id, RoomDto dto)
    {
        dto.RoomId = id;
        await _roomService.UpdateRoomAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteRoom(Guid id)
    {
        await _roomService.DeleteRoomAsync(id);
        return NoContent();
    }
}