using MassTransit;
using MySql.Data.MySqlClient;
using Polly;
using Polly.Extensions.Http;
using PosTech.Fase3.AddContact.API.Logging;
using PosTech.Fase3.AddContact.API.PolicyHandler;
using PosTech.Fase3.AddContact.Application.UseCases;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Infrastructure.Clients;
using PosTech.Fase3.AddContact.Infrastructure.Publications;
using PosTech.Fase3.AddContact.Infrastructure.Repositories;
using Serilog;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog(SeriLogger.ConfigureLogger);

builder.Services.AddHttpClient<ICodeAreaClient, CodeAreaClient>(c =>
                c.BaseAddress = new Uri(configuration["ApiSettings:AreaCode"]!))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy())
                .AddPolicyHandler(PolicyHandler.GetRetryPolicy());

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Mysql");
builder.Services.AddScoped<IDbConnection, MySqlConnection>((connection) => new MySqlConnection(connectionString));

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });       
    });
});

builder.Services.AddTransient<LoggingDelegatingHandler>();
builder.Services.AddTransient<IProtocolRepository, ProtocolRepository>();
builder.Services.AddTransient<ISaveContactPublisher, SaveContactPublisher>();
builder.Services.AddTransient<ISaveContactUseCase, SaveContactUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
