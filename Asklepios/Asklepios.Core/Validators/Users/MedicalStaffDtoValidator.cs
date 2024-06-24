using Asklepios.Core.DTO.Users;
using FluentValidation;

namespace Asklepios.Core.Validators.Users;

public class MedicalStaffDtoValidator : AbstractValidator<MedicalStaffDto>
{
    public MedicalStaffDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().WithMessage("Doctor Name is required.");
        RuleFor(dto => dto.Name).MinimumLength(3).MaximumLength(100).WithMessage("Nurse Name length must be between 3 and 100 characters.");
        RuleFor(dto => dto.Surname).NotEmpty().WithMessage("Doctor Surname is required.");
        RuleFor(dto => dto.Surname).MinimumLength(3).MaximumLength(100).WithMessage("Doctor Surname length must be between 3 and 100 characters.");
        RuleFor(dto => dto.PrivatePhoneNumber).NotEmpty().WithMessage("Private phone number is required.");
        RuleFor(dto => dto.PrivatePhoneNumber).MaximumLength(12).WithMessage("Private phone number length must be max 12 characters.");
        RuleFor(dto => dto.HospitalPhoneNumber).NotEmpty().WithMessage("Hospital phone number is required.");
        RuleFor(dto => dto.HospitalPhoneNumber).MaximumLength(12).WithMessage("Hospital phone number length must be max 12 characters.");
        RuleFor(dto => dto.Specialization).NotEmpty().WithMessage("Specialization is required.");
        RuleFor(dto => dto.Specialization).MaximumLength(200).WithMessage("Specialization length must be max 200 characters.");
        RuleFor(dto => dto.MedicalLicenseNumber).NotEmpty().WithMessage("Mecidal license number is required.");
        RuleFor(dto => dto.MedicalLicenseNumber).MaximumLength(12).WithMessage("Medical license number length must be max 12 characters.");
        RuleFor(dto => dto.UserId).NotEmpty().WithMessage("User Id is required.");
        RuleFor(dto => dto.DepartmentId).NotEmpty().WithMessage("Department Id is required.");
    }
}