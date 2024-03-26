using Asklepios.Core.DTO.Departments;
using FluentValidation;

namespace Asklepios.Core.Validators.Departments;

public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
{
    public DepartmentDtoValidator()
    {
        RuleFor(dto => dto.DepartmentName).NotEmpty().WithMessage("Department name is required.");
        RuleFor(dto => dto.DepartmentName).MinimumLength(3).MaximumLength(100).WithMessage("Department name length must be between 3 and 100 characters.");
        RuleFor(dto => dto.NumberOfBeds).NotEmpty().WithMessage("Numbers of bed is required.");
        RuleFor(dto => dto.ActualNumberOfPatients).NotEmpty().WithMessage("Numbers of bed is required.");
    }
}