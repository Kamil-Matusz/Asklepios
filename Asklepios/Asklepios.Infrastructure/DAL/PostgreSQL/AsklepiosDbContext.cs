using Asklepios.Core.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Asklepios.Infrastructure.DAL.PostgreSQL;

internal sealed class AsklepiosDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    
    public AsklepiosDbContext(DbContextOptions<AsklepiosDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}