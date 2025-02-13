using Asklepios.Core.Repositories.Departments;
using Asklepios.Core.Repositories.Examinations;
using Asklepios.Core.Repositories.Statistics;
using Asklepios.Core.Repositories.Users;
using Asklepios.Infrastructure.Redis.Repositories;
using Asklepios.Infrastructure.Redis.Repositories.Departments;
using Asklepios.Infrastructure.Redis.Repositories.Examinations;
using Asklepios.Infrastructure.Redis.Repositories.Statistics;
using Asklepios.Infrastructure.Redis.Repositories.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Infrastructure.Redis;

public static class Extensions
{
    private const string SectionName = "redis";
    
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = configuration.GetOptions<RedisOptions>(SectionName);
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisOptions.ConnectionString;
            options.InstanceName = redisOptions.InstanceName;
        });

        services.AddScoped<ISummaryCacheRepository, SummaryCacheRepository>();
        services.AddScoped<IStatisticsCacheRepository, StatisticsCacheRepository>();
        services.AddScoped<IAccountCacheRepository, AccountCacheRepository>();
        services.AddScoped<IMedicalStaffCacheRepository, MedicalStaffCacheRepository>();
        services.AddScoped<IDepartmentCacheRepository, DepartmentCacheRepository>();
        services.AddScoped<IRoomCacheRepository, RoomCacheRepository>();
        services.AddScoped<IExaminationCacheRepository, ExaminationCacheRepository>();

        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}