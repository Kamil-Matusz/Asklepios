using Asklepios.Infrastructure.Repositories.Departments;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDepartmentRepository, InMemoryDepartmentRepository>();
        services.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
        
        return services;
    }
}