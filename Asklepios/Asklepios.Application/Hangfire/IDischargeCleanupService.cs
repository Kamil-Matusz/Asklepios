namespace Asklepios.Application.Hangfire;

public interface IDischargeCleanupService
{
    Task RemoveOldDischarges();
}