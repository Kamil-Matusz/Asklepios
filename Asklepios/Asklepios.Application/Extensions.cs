using Asklepios.Application.Services.Departments;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDeparmentService, DepartmentService>();
        services.AddScoped<IRoomService, RoomService>();
        
        return services;
    }
}