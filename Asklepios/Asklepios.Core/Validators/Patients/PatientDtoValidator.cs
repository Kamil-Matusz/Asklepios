using Asklepios.Core.DTO.Patients;
using FluentValidation;

namespace Asklepios.Core.Validators.Patients;

public class PatientDtoValidator : AbstractValidator<PatientDto>
{
    public PatientDtoValidator()
    {
        RuleFor(dto => dto.PatientName).NotEmpty().WithMessage("Name is required.");
        RuleFor(dto => dto.PatientName).MinimumLength(3).MaximumLength(200).WithMessage("Name length must be between 3 and 200 characters.");
        RuleFor(dto => dto.PatientSurname).NotEmpty().WithMessage("Surname is required.");
        RuleFor(dto => dto.PatientSurname).MinimumLength(3).MaximumLength(200).WithMessage("Surname length must be between 3 and 200 characters.");
        RuleFor(dto => dto.PeselNumber).NotEmpty().Length(11).WithMessage("Pesel is required.");
        RuleFor(dto => dto.PeselNumber).Length(11).WithMessage("Surname length must 11 characters.");
        RuleFor(dto => dto.IsDischarged).NotEmpty().WithMessage("Information about discharged is required");
        RuleFor(dto => dto.DepartmentId).NotEmpty().WithMessage("Department Id is required.");
        RuleFor(dto => dto.RoomId).NotEmpty().WithMessage("Room Id is required.");
        RuleFor(dto => dto.MedicalStaffId).NotEmpty().WithMessage("Doctor Id is required.");
        RuleFor(dto => dto.Address).NotEmpty().WithMessage("Patient address is required.");
        RuleFor(dto => dto.Address).MinimumLength(3).MaximumLength(200).WithMessage("Address length must be between 3 and 200 characters.");
    }
}