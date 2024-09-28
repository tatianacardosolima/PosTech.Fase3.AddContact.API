using MassTransit;
using MySql.Data.MySqlClient;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using PosTech.Fase3.AddContact.API.Filters;
using PosTech.Fase3.AddContact.API.Logging;
using PosTech.Fase3.AddContact.API.PolicyHandler;
using PosTech.Fase3.AddContact.Application.UseCases;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Infrastructure.Clients;
using PosTech.Fase3.AddContact.Infrastructure.Publications;
using Prometheus;
using Serilog;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5011");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog(SeriLogger.ConfigureLogger);

builder.Services.AddHttpClient<ICodeAreaClient, CodeAreaClient>(c =>
                c.BaseAddress = new Uri(configuration["ApiSettings:AreaCode"]!))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy())
                .AddPolicyHandler(PolicyHandler.GetRetryPolicy());

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["rabbitmq:host"], "/", h =>
        {
            h.Username(configuration["rabbitmq:user"]!);
            h.Password(configuration["rabbitmq:password"]!);
        });

        cfg.ReceiveEndpoint("CreateContactEvent", e =>
        {
            e.ConfigureConsumeTopology = false; 
            e.Bind(configuration["rabbitmq:entityname"]!, s =>
            {
                s.RoutingKey = "CreateContactEvent"; 
                s.ExchangeType = "direct"; 
            });
        });
    });
});

builder.Services.AddTransient<LoggingDelegatingHandler>();
builder.Services.AddTransient<ISaveContactPublisher, SaveContactPublisher>();
builder.Services.AddTransient<ISaveContactUseCase, SaveContactUseCase>();

var app = builder.Build();
app.UseMetricServer();
app.UseHttpMetrics();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{
    protected Program() { }
}