using Asklepios.Infrastructure.DAL.PostgreSQL;

namespace Asklepios.Infrastructure.DAL.Seeders;

public interface IClearDatabase
{
    Task ClearDatabaseAsync(AsklepiosDbContext dbContext);
}