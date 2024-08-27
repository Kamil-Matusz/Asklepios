using Asklepios.Application.Abstractions;
using Asklepios.Application.Services.Clock;
using Asklepios.Application.Services.Departments;
using Asklepios.Application.Services.Email;
using Asklepios.Application.Services.Email.SendGrid;
using Asklepios.Application.Services.Examinations;
using Asklepios.Application.Services.Notification;
using Asklepios.Application.Services.Patients;
using Asklepios.Application.Services.Statistics;
using Asklepios.Application.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ICommandHandler<>).Assembly;
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddScoped<IDeparmentService, DepartmentService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddSingleton<IClock, Clock>();
        services.AddScoped<INurseService, NurseService>();
        services.AddScoped<IMedicalStaffService, MedicalStaffService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IExaminationService, ExaminationService>();
        services.AddScoped<IExamResultService, ExamResultService>();
        services.AddScoped<IOperationService, OperationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IDischargeSummaryService, DischargeSummaryService>();

        services.AddSendGrid(configuration);
        
        return services;
    }
}