using Asklepios.Application.Abstractions;
using Asklepios.Application.Commands.Discharges;
using Asklepios.Application.PDF;
using Asklepios.Application.Queries.Discharges;
using Asklepios.Application.Services.Examinations;
using Asklepios.Application.Services.Patients;
using Asklepios.Application.Services.Users;
using Asklepios.Core.DTO.Patients;
using Convey.MessageBrokers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using UpdateDischargeStatus = Asklepios.Application.Commands.Discharges.UpdateDischargeStatus;

namespace Asklepios.Api.Controllers.Patients;

public class DischargesController : BaseController
{
    private readonly ICommandHandler<AddDischarge> _addDischargeHandler;
    private readonly ICommandHandler<DeleteDischarge> _deleteDischargeHandler;
    private readonly ICommandHandler<UpdateDischarge> _updateDischargeHandler;
    private readonly IQueryHandler<GetDischargeById, DischargeItemDto> _getDischargeByIdHandler;
    private readonly IQueryHandler<GetDischargeByPesel, DischargeItemDto> _getDischargeByPeselHandler;
    private readonly ICommandHandler<UpdateDischargeStatus> _updateDischargeStatusHandler;
    private readonly IQueryHandler<GetDoctorDischarges, IEnumerable<DischargeItemDto>> _getDoctorDischarges;
    private readonly IQueryHandler<GetAllDischarges, IEnumerable<DischargeItemDto>> _getAllDischarges;
    private readonly IPatientService _patientService;
    private readonly IMedicalStaffService _medicalStaffService;
    private readonly IPatientHistoryService _patientHistoryService;
    private readonly IOperationService _operationService;

    public DischargesController(ICommandHandler<AddDischarge> addDischargeHandler, ICommandHandler<DeleteDischarge> deleteDischargeHandler, IQueryHandler<GetDischargeById, DischargeItemDto> getDischargeByIdHandler, IQueryHandler<GetDischargeByPesel, DischargeItemDto> getDischargeByPeselHandler, IPatientService patientService, ICommandHandler<UpdateDischarge> updateDischargeHandler, ICommandHandler<UpdateDischargeStatus> updateDischargeStatusHandler, IMedicalStaffService medicalStaffService, IQueryHandler<GetDoctorDischarges, IEnumerable<DischargeItemDto>> getDoctorDischarges, IQueryHandler<GetAllDischarges, IEnumerable<DischargeItemDto>> getAllDischarges, IPatientHistoryService patientHistoryService, IOperationService operationService)
    {
        _addDischargeHandler = addDischargeHandler;
        _deleteDischargeHandler = deleteDischargeHandler;
        _getDischargeByIdHandler = getDischargeByIdHandler;
        _getDischargeByPeselHandler = getDischargeByPeselHandler;
        _patientService = patientService;
        _updateDischargeHandler = updateDischargeHandler;
        _updateDischargeStatusHandler = updateDischargeStatusHandler;
        _medicalStaffService = medicalStaffService;
        _getDoctorDischarges = getDoctorDischarges;
        _getAllDischarges = getAllDischarges;
        _patientHistoryService = patientHistoryService;
        _operationService = operationService;
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> AddDischarge(AddDischarge command)
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var doctorId = await _medicalStaffService.GetDoctorIdAsync(userId);
        command = command with {DischargeId = Guid.NewGuid(), MedicalStaffId = doctorId};
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
    [HttpGet("{dischargeId:guid}")]
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
    
    [Authorize(Roles = "Doctor, Admin")]
    [HttpPost("dischargePatient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DischargePerson(DischargePersonDto dto)
    {
        var patients = await _patientService.GetPatientDataAsync(dto.PatientId);
        var userId = Guid.Parse(User.Identity?.Name);
        var doctorId = await _medicalStaffService.GetDoctorIdAsync(userId);
        bool status = true;

        var addDischargeCommand = new AddDischarge(
            DischargeId: Guid.NewGuid(),
            PatientName: patients.PatientName,
            PatientSurname: patients.PatientSurname,
            PeselNumber: patients.PeselNumber,
            Address: patients.Address,
            Date: DateOnly.FromDateTime(DateTime.Now),
            DischargeReasson: dto.DischargeReasson,
            Summary: dto.Summary,
            MedicalStaffId: doctorId
        );

        var updateDischargeStatusCommand = new UpdateDischargeStatus(
            PatientId: dto.PatientId,
            IsDischarged: status
        );
        
        await _addDischargeHandler.HandlerAsync(addDischargeCommand);
        await _updateDischargeStatusHandler.HandlerAsync(updateDischargeStatusCommand);

        var operations = await _operationService.GetAllOperationsByPatientAsync(patients.PatientId);
        
        var patientVisits = operations.Select(operation => new PatientVisitDto
        {
            AdmissionDate = DateOnly.FromDateTime(DateTime.Now),
            DischargeDate = DateOnly.FromDateTime(DateTime.Now).AddDays(2),
            OperationName = operation.OperationName,
            Result = operation.Result,
            Comlications = operation.Complications,
            AnesthesiaType = operation.AnesthesiaType
        }).ToList();
        
        var patientHistoryDto = new PatientHistoryDto
        {
            PatientName = patients.PatientName,
            PatientSurname = patients.PatientSurname,
            PeselNumber = patients.PeselNumber,
            History = patientVisits
        };

        await _patientHistoryService.AddOrUpdatePatientHistoryAsync(patientHistoryDto);
    
        return Ok();
    }

    [Authorize(Roles = "Doctor")]
    [HttpGet("yoursDischarges")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<DischargeItemDto>>> GetDoctorDischarges(
        [FromQuery] GetDoctorDischarges query)
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var doctorId = await _medicalStaffService.GetDoctorIdAsync(userId);
        query.DoctorId = doctorId;

        var discharges = await _getDoctorDischarges.HandlerAsync(query);

        return Ok(discharges);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("allDischarges")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<DischargeItemDto>>> GetAllDischarges([FromQuery] GetAllDischarges query)
        => Ok(await _getAllDischarges.HandlerAsync(query));
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet("{dischargeId:guid}/dischargePDF")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDischargePdf(Guid dischargeId)
    {
        var discharge = await _getDischargeByIdHandler.HandlerAsync(new GetDischargeById
        {
            DischargeId = dischargeId
        });

        if (discharge is null)
        {
            return NotFound($"Nie znaleziono wypisu o ID: {dischargeId}");
        }
        
        var document = new DischargePdfDocument(discharge);
        var pdfBytes = document.GeneratePdf();
        
        return File(pdfBytes, "application/pdf", $"Wypis-{discharge.PatientName} {discharge.PatientSurname}.pdf");
    }
}