using Asklepios.Infrastructure.DAL.PostgreSQL;
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