using Asklepios.Application.Hangfire;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Storage;
using Moq;

namespace Asklepios.InfrastructureTests.Hangfire;

public class HangfireRecurringJobTests
{
    public HangfireRecurringJobTests()
    {
        GlobalConfiguration.Configuration.UseMemoryStorage();
    }

    [Fact]
    public void Should_Register_RecurringJob_ForDischargeCleanup()
    {
        // Arrange
        var serviceMock = new Mock<IDischargeCleanupService>();
        
        // Act
        RecurringJob.AddOrUpdate<IDischargeCleanupService>(x => x.RemoveOldDischarges(), Cron.Daily);
        
        var recurringJob = JobStorage.Current.GetConnection().GetRecurringJobs();
        
        // Assert
        Assert.Contains(recurringJob, job => job.Id.Contains(nameof(IDischargeCleanupService) + ".RemoveOldDischarges"));
    }
    
    [Fact]
    public void Should_Remove_RecurringJob_ForDischargeCleanup()
    {
        // Arrange
        RecurringJob.AddOrUpdate<IDischargeCleanupService>(x => x.RemoveOldDischarges(), Cron.Daily);
    
        // Act
        RecurringJob.RemoveIfExists(nameof(IDischargeCleanupService) + ".RemoveOldDischarges");
    
        var recurringJob = JobStorage.Current.GetConnection().GetRecurringJobs();

        // Assert
        Assert.DoesNotContain(recurringJob, job => job.Id.Contains(nameof(IDischargeCleanupService) + ".RemoveOldDischarges"));
    }
}