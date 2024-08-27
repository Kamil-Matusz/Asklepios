using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Asklepios.Infrastructure.DAL.PostgreSQL;

public sealed class AsklepiosDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Nurse> Nurses { get; set; }
    public DbSet<MedicalStaff> MedicalStaff { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Discharge> Discharges { get; set; }
    public DbSet<Examination> Examinations { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<Operation> Operations { get; set; }
    
    // Views
    public DbSet<MonthlyDischargeSummary> MonthlyDischarges { get; set; }
    
    public AsklepiosDbContext(DbContextOptions<AsklepiosDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        modelBuilder.Entity<MonthlyDischargeSummary>()
            .HasNoKey()
            .ToView("MonthlyDischarges");
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(p => p.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}