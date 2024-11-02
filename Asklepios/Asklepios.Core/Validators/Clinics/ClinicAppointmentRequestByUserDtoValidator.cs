using System.Drawing;
using Asklepios.Core.DTO.Clinics;
using FluentValidation;

namespace Asklepios.Core.Validators.Clinics;

public class ClinicAppointmentRequestByUserDtoValidator : AbstractValidator<ClinicAppointmentRequestByUserDto>
{
    public ClinicAppointmentRequestByUserDtoValidator()
    {
        RuleFor(dto => dto.PatientName)
            .NotEmpty().WithMessage("Patient name is required.")
            .MinimumLength(3).WithMessage("Patient name must be at least 3 characters long.")
            .MaximumLength(200).WithMessage("Patient name must be at most 200 characters long.");
            
        RuleFor(dto => dto.PatientSurname)
            .NotEmpty().WithMessage("Patient surname is required.")
            .MinimumLength(3).WithMessage("Patient surname must be at least 3 characters long.")
            .MaximumLength(200).WithMessage("Patient surname must be at most 200 characters long.");

        RuleFor(dto => dto.PeselNumber)
            .NotEmpty().WithMessage("Pesel number is required.")
            .Length(11).WithMessage("Pesel number must be exactly 11 characters long.");

        RuleFor(dto => dto.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Contact number must be a valid phone number.");

        RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
            
        RuleFor(dto => dto.AppointmentDate)
            .GreaterThan(DateTime.Now).WithMessage("Appointment date must be in the future.");
            
        RuleFor(dto => dto.AppointmentType)
            .IsInEnum().WithMessage("Invalid appointment type.");
            
        RuleFor(dto => dto.MedicalStaffId)
            .NotEmpty().WithMessage("Medical staff ID is required.");
            
        RuleFor(dto => dto.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => status == "Scheduled" || status == "Confirmed" || status == "Cancelled")
            .WithMessage("Status must be 'Scheduled', 'Confirmed', or 'Cancelled'.");
    }
}