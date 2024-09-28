using Asklepios.Application.Events;
using Asklepios.Infrastructure.Events.Handlers;
using Convey.CQRS.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Infrastructure.Events;

public static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        var infrastructureAssembly = typeof(AppOptions).Assembly;
        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}