using Asklepios.Core.DTO.Users;
using Asklepios.Core.Policies;
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
        services.AddFluentValidation();
        services.AddPolicies();
        
        return services;
    }
}