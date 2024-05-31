using Asklepios.Core.Repositories.Departments;
using Asklepios.Core.Repositories.Examinations;
using Asklepios.Core.Repositories.Patients;
using Asklepios.Core.Repositories.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Asklepios.Infrastructure.DAL.Repositories;
using Asklepios.Infrastructure.DAL.Repositories.Departments;
using Asklepios.Infrastructure.DAL.Repositories.Examinations;
using Asklepios.Infrastructure.DAL.Repositories.Patients;
using Asklepios.Infrastructure.DAL.Repositories.Users;
using Asklepios.Infrastructure.DAL.Seeders;
using Asklepios.Infrastructure.DAL.Seeders.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Infrastructure.DAL;

public static class Extensions
{
    private const string SectionName = "postgres";

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<PostgresOptions>(section);
        var options = configuration.GetOptions<PostgresOptions>(SectionName);
        
        services.AddDbContext<AsklepiosDbContext>(x => x.UseNpgsql(options.ConnectionString));

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<INurseRepository, NurseRepository>();
        services.AddScoped<IMedicalStaffRepository, MedicalStaffRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDischargeRepository, DischargeRepository>();
        services.AddScoped<IExaminationRepository, ExaminationRepository>();
        services.AddScoped<IExamResultRepository, ExamResultRepository>();
        services.AddScoped<IOperationRepository, OperationRepository>();
        
        services.AddTransient<IDataSeeder, UsersSeeder>();
        
        services.AddHostedService<DatabaseInitializer>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}