using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.IntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                services.AddHttpClient<ICodeAreaClient, MockCodeAreaClient>(c =>
                {
                    // Pode definir o BaseAddress para um URI de mock ou uma URL de API falsa para testes.
                    c.BaseAddress = new Uri("https://mock-api/");
                });
            });
        }
    }
}
