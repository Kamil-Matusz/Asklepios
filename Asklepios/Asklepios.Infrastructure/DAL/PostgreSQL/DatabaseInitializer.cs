using Asklepios.Infrastructure.DAL.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Asklepios.Infrastructure.DAL.PostgreSQL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<IDataSeeder> _dataSeeders;

    public DatabaseInitializer(IServiceProvider serviceProvider, IEnumerable<IDataSeeder> dataSeeders)
    {
        _serviceProvider = serviceProvider;
        _dataSeeders = dataSeeders.OrderBy(s => s is IOrderedSeeder orderedSeeder ? orderedSeeder.Order : int.MaxValue).ToList();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // auto migrations & seeders
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AsklepiosDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);
            
            foreach (var seeder in _dataSeeders)
            {
                await seeder.SeedAsync(dbContext);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}