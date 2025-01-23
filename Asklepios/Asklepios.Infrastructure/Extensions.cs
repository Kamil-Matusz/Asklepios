using Asklepios.Application.Abstractions;
using Asklepios.Application.Hangfire;
using Asklepios.Application.SignalR;
using Asklepios.Infrastructure.Auth;
using Asklepios.Infrastructure.DAL;
using Asklepios.Infrastructure.Errors;
using Asklepios.Infrastructure.Events;
using Asklepios.Infrastructure.Redis;
using Asklepios.Infrastructure.Security;
using Convey;
using Convey.CQRS.Events;
using Convey.MessageBrokers.RabbitMQ;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Asklepios.Infrastructure;

public static class Extensions
{
    private const string CorsPolicy = "cors";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var infrastructureAssembly = typeof(AppOptions).Assembly;
        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        // PostgreSQL
        services.AddPostgres(configuration);
        
        // Redis
        services.AddRedis(configuration);
        
        // CORS
        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("http://localhost:8080", "http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
        
        // Swagger Documentation
        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Asklepios - Hospital Managment System",
                Version = "v1"
            });
            
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        
        services.AddErrorHandling();
        services.AddControllers();
        services.AddSecurity();
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();
        
        // RabbitMQ
        services.AddConvey()
            .AddRabbitMq()
            .AddEventHandlers()
            .Build();

        services.AddEvents();

        services.AddSignalR();
        
        return services;
    }
    
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicy);

        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.RoutePrefix = "swagger";
            swagger.DocumentTitle = "Asklepios - Hospital Managment System";
        });
        
        app.UseErrorHandling();
        
        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseConvey();
        app.UseRabbitMq();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<HospitalHub>("/hospitalHub");
        });
        
        var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
    
        var hangfireEnabled = configuration.GetValue<bool>("Hangfire:Enable");
        if (hangfireEnabled)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        
            RecurringJob.AddOrUpdate<IDischargeCleanupService>(x => x.RemoveOldDischarges(), Cron.Daily);
            RecurringJob.AddOrUpdate<IClinicAppointmentsCleanupService>(x => x.RemoveOldAppointments(), Cron.Daily);
            RecurringJob.AddOrUpdate<IClinicAppointmentScheduler>(x => x.RemindAboutVisit(), Cron.Daily(7));
        }
        
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}