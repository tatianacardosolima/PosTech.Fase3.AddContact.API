using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PosTech.Fase3.AddContact.Domain.Interfaces;

namespace PosTech.Fase3.AddContact.IntegrationTests.Base
{
    public class MyCustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o serviço real do contêiner
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(ICodeAreaClient));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddTransient<ICodeAreaClient, MockCodeAreaClient>();
            });
        }
    }
}
