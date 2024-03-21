using Asklepios.Core.DTO.Departments;
using FluentValidation;

namespace Asklepios.Core.Validators.Departments;

public class RoomDtoValidator : AbstractValidator<RoomDto>
{
    public RoomDtoValidator()
    {
        RuleFor(dto => dto.RoomNumber).NotEmpty().WithMessage("Room number is required.");
        RuleFor(dto => dto.RoomType).NotEmpty().WithMessage("Room type is required.");
        RuleFor(dto => dto.RoomType).MinimumLength(3).MaximumLength(100).WithMessage("Room type length must be between 3 and 100 characters.");
        RuleFor(dto => dto.DepartmentId).NotEmpty().WithMessage("Department Id is required.");
    }
}