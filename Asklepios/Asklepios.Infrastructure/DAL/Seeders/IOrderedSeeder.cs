namespace Asklepios.Infrastructure.DAL.Seeders;

public interface IOrderedSeeder : IDataSeeder
{
    int Order { get; }
}
