using Asklepios.Core.Policies.Departments;
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
            .RegisterValidatorsFromAssemblyContaining<UserDtoValidator>());
        
        services.AddSingleton<IDepartmentDeletePolicy, DepartmentDeletionPolicy>();
        
        return services;
    }
}