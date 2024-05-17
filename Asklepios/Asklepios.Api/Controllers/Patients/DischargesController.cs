using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Discharges;
using Asklepios.Application.Queries.Discharges;
using Asklepios.Core.DTO.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asklepios.Api.Controllers.Patients;

public class DischargesController : BaseController
{
    private readonly ICommandHandler<AddDischarge> _addDischargeHandler;
    private readonly ICommandHandler<DeleteDischarge> _deleteDischargeHandler;
    private readonly ICommandHandler<UpdateDischarge> _updateDischargeHandler;
    private readonly IQueryHandler<GetDischargeById, DischargeItemDto> _getDischargeByIdHandler;
    private readonly IQueryHandler<GetDischargeByPesel, DischargeItemDto> _getDischargeByPeselHandler;

    public DischargesController(ICommandHandler<AddDischarge> addDischargeHandler, ICommandHandler<DeleteDischarge> deleteDischargeHandler, ICommandHandler<UpdateDischarge> updateDischargeHandler, IQueryHandler<GetDischargeById, DischargeItemDto> getDischargeByIdHandler, IQueryHandler<GetDischargeByPesel, DischargeItemDto> getDischargeByPeselHandler)
    {
        _addDischargeHandler = addDischargeHandler;
        _deleteDischargeHandler = deleteDischargeHandler;
        _updateDischargeHandler = updateDischargeHandler;
        _getDischargeByIdHandler = getDischargeByIdHandler;
        _getDischargeByPeselHandler = getDischargeByPeselHandler;
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddDischarge(AddDischarge command)
    {
        command = command with {DischargeId = Guid.NewGuid()};
        await _addDischargeHandler.HandlerAsync(command);
        return Ok();
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete("{dischargeId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteDischarge(Guid dischargeId, DeleteDischarge command)
    {
        await _deleteDischargeHandler.HandlerAsync(command with { DischargeId = dischargeId});
        return NoContent();
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet("dischargesById/{dischargeId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DischargeItemDto>> GetDischargeById(Guid dischargeId)
    {
        var discharge = await _getDischargeByIdHandler.HandlerAsync(new GetDischargeById() {DischargeId = dischargeId});
        if (discharge is null)
        {
            return NotFound();
        }

        return Ok(discharge);
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet("dischargesByPeselNumber/{peselNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DischargeItemDto>> GetDischargeByPeselNumber(string peselNumber)
    {
        var discharge = await _getDischargeByPeselHandler.HandlerAsync(new GetDischargeByPesel() {PeselNumber = peselNumber});
        if (discharge is null)
        {
            return NotFound();
        }

        return Ok(discharge);
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut("{dischargeId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateDischarge(Guid dischargeId, UpdateDischarge command)
    {
        await _updateDischargeHandler.HandlerAsync(command with { DischargeId = dischargeId});
        return Ok();
    }
}