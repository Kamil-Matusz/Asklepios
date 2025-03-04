using Asklepios.Application.Services.Email;
using Asklepios.Core.Repositories.Clinics;

namespace Asklepios.Application.Hangfire;

public class ClinicAppointmentScheduler : IClinicAppointmentScheduler
{
    private readonly IClinicAppointmentRepository _clinicAppointmentRepository;
    private readonly IEmailService _emailService;

    public ClinicAppointmentScheduler(IClinicAppointmentRepository clinicAppointmentRepository, IClinicPatientRepository clinicPatientRepository, IEmailService emailService)
    {
        _clinicAppointmentRepository = clinicAppointmentRepository;
        _emailService = emailService;
    }

    public async Task RemindAboutVisit()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        var appointments = await _clinicAppointmentRepository.GetAppointmentsByDateAsync(tomorrow);
        
        foreach (var appointment in appointments)
        {
            _emailService.SendEmailWithReminderAboutVisitAsync(appointment.ClinicPatient.Email,
                appointment.ClinicPatient.PatientName, appointment.ClinicPatient.PatientSurname,
                appointment.AppointmentDate.ToString(), appointment.MedicalStaff.Name, appointment.MedicalStaff.Surname);
        }
    }
}