using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Asklepios.Infrastructure.DAL.PostgreSQL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // auto migration to the database
        using (var scope = _serviceProvider.CreateScope()) {
            var dbContext = scope.ServiceProvider.GetRequiredService<AsklepiosDbContext>();
            dbContext.Database.Migrate();
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}