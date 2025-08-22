using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Asklepios.Infrastructure.Logging;

public static class Extensions
{
    public static IServiceCollection AddSeqLogging(this IServiceCollection services, IConfiguration configuration)
    {
        var seqOptions = configuration.GetOptions<SeqOptions>("Logging:Seq");
        services.AddSingleton(seqOptions);
        
        var fileSection = configuration.GetSection("Logging:File");
        var logFilePath = fileSection.GetValue<string>("Path", "logs/log-.txt");
        var rollingInterval = fileSection.GetValue("RollingInterval", RollingInterval.Day);
        var outputTemplate = fileSection.GetValue<string>("OutputTemplate");

        var logLevel = Enum.TryParse<LogEventLevel>(seqOptions.MinimumLevel, true, out var level)
            ? level
            : LogEventLevel.Information;

        var logger = new LoggerConfiguration()
            .MinimumLevel.Is(logLevel)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.WithProperty("Service", seqOptions.ServiceName)
            .Enrich.WithProperty("Environment", seqOptions.Environment)
            .WriteTo.Console()
            .WriteTo.File(
                path: logFilePath,
                rollingInterval: rollingInterval,
                restrictedToMinimumLevel: logLevel,
                outputTemplate: outputTemplate
            )
            .WriteTo.Seq(seqOptions.ServerUrl, apiKey: seqOptions.ApiKey)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(logger, dispose: true);
        });

        return services;
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}
