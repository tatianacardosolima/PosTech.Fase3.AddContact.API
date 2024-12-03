using System.Diagnostics.CodeAnalysis;
using PosTech.Fase3.AddContact.API.HealthChecks;

namespace PosTech.Fase3.AddContact.API.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class HealthCheckSetup
    {
        internal static void AddRabbitMQHealthCheck(this IHealthChecksBuilder healthChecks)
        {
            healthChecks.AddCheck<MassTransitRabbitMqHealthCheck>(nameof(MassTransitRabbitMqHealthCheck));
        }
    }
}