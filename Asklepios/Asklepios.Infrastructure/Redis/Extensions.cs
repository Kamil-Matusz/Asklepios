using Asklepios.Core.Repositories.Statistics;
using Asklepios.Infrastructure.Redis.Repositories;
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

        services.AddScoped<IRedisSummaryRepository, RedisSummaryRepository>();
        services.AddScoped<IStatisticsCacheRepository, StatisticsCacheRepository>();

        return services;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}