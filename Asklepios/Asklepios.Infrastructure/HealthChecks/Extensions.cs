using Asklepios.Infrastructure.DAL.PostgreSQL;
using Asklepios.Infrastructure.Redis;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Asklepios.Infrastructure.HealthChecks;

public static class Extensions
{
    private const string PostgresSection = "postgres";
    private const string RedisSection = "redis";
    
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresOptions = configuration.GetOptions<PostgresOptions>(PostgresSection);
        var redisOptions = configuration.GetOptions<RedisOptions>(RedisSection);

        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: postgresOptions.ConnectionString,
                name: "PostgreSQL",
                failureStatus: HealthStatus.Unhealthy)
            .AddRedis(
                redisConnectionString: redisOptions.ConnectionString,
                name: "Redis",
                failureStatus: HealthStatus.Degraded)
            .AddCheck<SignalRHealthCheck>("SignalR");

        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
    }
}