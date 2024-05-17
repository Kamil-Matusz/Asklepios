using Asklepios.Core.DTO.Examinations;
using FluentValidation;

namespace Asklepios.Core.Validators.Examinations;

public class ExaminationDtoValidator : AbstractValidator<ExaminationDto>
{
    public ExaminationDtoValidator()
    {
        RuleFor(dto => dto.ExamName).NotEmpty().WithMessage("Exam name is required.");
        RuleFor(dto => dto.ExamName).MinimumLength(3).MaximumLength(200).WithMessage("Exam name length must be between 3 and 200 characters.");
    }
}