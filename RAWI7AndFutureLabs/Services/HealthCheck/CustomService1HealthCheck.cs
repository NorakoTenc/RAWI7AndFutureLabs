﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace RAWI7AndFutureLabs.Services.HealthCheck
{
    public class CustomService1HealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isHealthy = true;

            return Task.FromResult(isHealthy
                ? HealthCheckResult.Healthy("Service 1 is healthy.")
                : HealthCheckResult.Unhealthy("Service 1 is unhealthy."));
        }
    }
}
