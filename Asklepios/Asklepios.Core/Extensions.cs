using Asklepios.Core.Policies.Departments;
using Asklepios.Core.Validators.Departments;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv
            .RegisterValidatorsFromAssemblyContaining<DepartmentDtoValidator>()
            .RegisterValidatorsFromAssemblyContaining<RoomDtoValidator>());
        
        services.AddSingleton<IDepartmentDeletePolicy, DepartmentDeletionPolicy>();
        
        return services;
    }
}