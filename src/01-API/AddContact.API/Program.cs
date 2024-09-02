using Polly;
using Polly.Extensions.Http;
using PosTech.Fase3.AddContact.API.Logging;
using PosTech.Fase3.AddContact.API.PolicyHandler;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Infrastructure.Clients;
using Serilog;

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
