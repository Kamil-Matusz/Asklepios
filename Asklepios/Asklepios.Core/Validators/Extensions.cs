using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Validators.Departments;
using Asklepios.Core.Validators.Examinations;
using Asklepios.Core.Validators.Patients;
using Asklepios.Core.Validators.Users;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Core.Validators;

public static class Extensions
{
    public static IServiceCollection AddFluentValidator(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<DepartmentDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<RoomDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<UserDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<NurseDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<MedicalStaffDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<PatientDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<DischargeDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<ExaminationDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<ExamResultDto>());

        return services;
    }
}