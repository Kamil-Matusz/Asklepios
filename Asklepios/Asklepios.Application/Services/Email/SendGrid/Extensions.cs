using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Asklepios.Application.Services.Email.SendGrid;

public static class Extensions
{
    private const string SendGridSectionName = "sendgrid";

    public static IServiceCollection AddSendGrid(this IServiceCollection services, IConfiguration configuration)
    {
        var sendGridOptions = configuration.GetOptions<SendGridOptions>(SendGridSectionName);
        services.AddSingleton(sendGridOptions);

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