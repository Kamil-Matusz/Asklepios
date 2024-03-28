using Asklepios.Infrastructure.DAL;
using Asklepios.Infrastructure.Repositories.Departments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        
        services.AddSingleton<IDepartmentRepository, InMemoryDepartmentRepository>();
        services.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
        
        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}