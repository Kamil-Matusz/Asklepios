using Asklepios.Core.DTO.Patients;
using FluentValidation;

namespace Asklepios.Core.Validators.Patients;

public class DischargeDtoValidator : AbstractValidator<DischargeDto>
{
    public DischargeDtoValidator()
    {
        RuleFor(dto => dto.PatientName).NotEmpty().WithMessage("Name is required.");
        RuleFor(dto => dto.PatientName).MinimumLength(3).MaximumLength(200).WithMessage("Name length must be between 3 and 200 characters.");
        RuleFor(dto => dto.PatientSurname).NotEmpty().WithMessage("Surname is required.");
        RuleFor(dto => dto.PatientSurname).MinimumLength(3).MaximumLength(200).WithMessage("Surname length must be between 3 and 200 characters.");
        RuleFor(dto => dto.Address).NotEmpty().WithMessage("Patient address is required.");
        RuleFor(dto => dto.Address).MinimumLength(3).MaximumLength(200).WithMessage("Address length must be between 3 and 200 characters.");
        RuleFor(dto => dto.Date).NotEmpty().WithMessage("Discharged date is required.");
        RuleFor(dto => dto.DischargeReasson).NotEmpty().WithMessage("Discharge reason is required.");
        RuleFor(dto => dto.DischargeReasson).MinimumLength(3).MaximumLength(2000).WithMessage("Reason length must be between 3 and 2000 characters.");
        RuleFor(dto => dto.Summary).NotEmpty().WithMessage("Patient summary is required.");
        RuleFor(dto => dto.Summary).MinimumLength(3).MaximumLength(5000).WithMessage("Summary length must be between 3 and 5000 characters.");
        RuleFor(dto => dto.MedicalStaffId).NotEmpty().WithMessage("Doctor Id is required.");
    }
}