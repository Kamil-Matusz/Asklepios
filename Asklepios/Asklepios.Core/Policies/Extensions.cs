using Asklepios.Core.Policies.Departments;
using Asklepios.Core.Policies.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Core.Policies;

public static class Extensions
{
    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddSingleton<IDepartmentDeletePolicy, DepartmentDeletionPolicy>();
        services.AddScoped<IUserDeletionPolicy, UserDeletionPolicy>();
        services.AddScoped<IRolePolicy, RolePolicy>();
        
        return services;
    }
}