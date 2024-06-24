using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders;

public interface IDataSeeder
{
    Task SeedAsync(AsklepiosDbContext dbContext);
}