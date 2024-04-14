using Asklepios.Core.DTO.Users;
using FluentValidation;

namespace Asklepios.Core.Validators.Users;

public class NurseDtoValidator : AbstractValidator<NurseDto>
{
    public NurseDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Nurse Name is required.");
        RuleFor(dto => dto.Name).MinimumLength(3).MaximumLength(100).WithMessage("Nurse Name length must be between 3 and 100 characters.");
        RuleFor(dto => dto.Surname).NotEmpty().WithMessage("Nurse Surname is required.");
        RuleFor(dto => dto.Surname).MinimumLength(3).MaximumLength(100).WithMessage("Nurse Surname length must be between 3 and 100 characters.");
        RuleFor(dto => dto.UserId).NotEmpty().WithMessage("User Id is required.");
        RuleFor(dto => dto.DepartmentId).NotEmpty().WithMessage("Department Id is required.");
    }
}