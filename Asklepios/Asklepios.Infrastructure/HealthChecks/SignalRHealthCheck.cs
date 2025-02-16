using Asklepios.Application.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Asklepios.Infrastructure.HealthChecks;

public class SignalRHealthCheck : IHealthCheck
{
    private readonly IHubContext<HospitalHub> _hubContext;

    public SignalRHealthCheck(IHubContext<HospitalHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (_hubContext != null)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("SignalR Hub is not available"));
    }
}