using Asklepios.Application.Abstractions;
using Asklepios.Application.Services.Clock;
using Asklepios.Application.Services.Departments;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ICommandHandler<>).Assembly;
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddScoped<IDeparmentService, DepartmentService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddSingleton<IClock, Clock>();
        
        return services;
    }
}