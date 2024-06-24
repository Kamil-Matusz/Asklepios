using Asklepios.Core.DTO.Examinations;
using FluentValidation;

namespace Asklepios.Core.Validators.Examinations;

public class OperationDtoValidator : AbstractValidator<OperationDto>
{
    public OperationDtoValidator()
    {
        RuleFor(dto => dto.OperationName).NotEmpty().WithMessage("Operation name is required.");
        RuleFor(dto => dto.OperationName).MinimumLength(1).MaximumLength(2000).WithMessage("Operation name length must be between 3 and 2000 characters.");
        RuleFor(dto => dto.StartDate).NotEmpty().WithMessage("Start date is required.");
        RuleFor(dto => dto.EndDate).NotEmpty().WithMessage("End date is required.");
        RuleFor(dto => dto.Result).NotEmpty().WithMessage("Result is required.");
        RuleFor(dto => dto.Result).MinimumLength(3).MaximumLength(2000).WithMessage("Exam name length must be between 3 and 2000 characters.");
        RuleFor(dto => dto.PatientId).NotEmpty().WithMessage("Patient Id is required.");
        RuleFor(dto => dto.MedicalStaffId).NotEmpty().WithMessage("Doctor Id is required.");
    }
}