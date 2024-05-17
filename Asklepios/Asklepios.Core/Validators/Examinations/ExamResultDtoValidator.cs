using Asklepios.Core.DTO.Examinations;
using FluentValidation;

namespace Asklepios.Core.Validators.Examinations;

public class ExamResultDtoValidator : AbstractValidator<ExamResultDto>
{
    public ExamResultDtoValidator()
    {
        RuleFor(dto => dto.PatientId).NotEmpty().WithMessage("Patient Id is required.");
        RuleFor(dto => dto.Date).NotEmpty().WithMessage("Discharged date is required.").Must(BeValidDate).WithMessage("Invalid date format.");
        RuleFor(dto => dto.Result).NotEmpty().MinimumLength(3).MaximumLength(200).WithMessage("Exam result length must be between 3 and 200 characters.");
        RuleFor(dto => dto.Comment).NotEmpty().MinimumLength(3).MaximumLength(200).WithMessage("Comment length must be between 3 and 200 characters.");
        RuleFor(dto => dto.ExamId).NotEmpty().WithMessage("Exam Id is required.");
    }
    
    private bool BeValidDate(DateOnly date)
    {
        return date != default(DateOnly);
    }
}