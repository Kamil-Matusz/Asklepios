using Asklepios.Core.DTO.Users;
using Asklepios.Core.Policies.Departments;
using Asklepios.Core.Policies.Users;
using Asklepios.Core.Validators.Departments;
using Asklepios.Core.Validators.Users;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<DepartmentDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<RoomDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<UserDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<NurseDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<MedicalStaffDtoValidator>());
        
        services.AddSingleton<IDepartmentDeletePolicy, DepartmentDeletionPolicy>();
        services.AddScoped<IUserDeletionPolicy, UserDeletionPolicy>();
        
        return services;
    }
}