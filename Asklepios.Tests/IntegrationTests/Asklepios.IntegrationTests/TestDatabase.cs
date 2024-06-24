using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.IntegrationTests;

public class TestDatabase : IDisposable
{
    public AsklepiosDbContext DbContext { get; }

    public TestDatabase()
    {
        var options = new OptionsProvider().Get<PostgresOptions>("postgres");
        DbContext = new AsklepiosDbContext(new DbContextOptionsBuilder<AsklepiosDbContext>().UseNpgsql(options.ConnectionString).Options);
    }
    
    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}