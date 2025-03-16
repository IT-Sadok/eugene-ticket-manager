using EventService.Api.Controllers;
using EventService.Application.Extensions;
using EventService.Domain.Messages;
using EventService.Domain.RepositoryContracts;
using EventService.Infrastructure.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var isLocalEnvironment = builder.Environment.EnvironmentName == "local";
var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();
    x.AddRider(rider =>
    {
        rider.AddProducer<ReserveTicketRequest>("ticket-reservation-request");

        rider.UsingKafka((context, cfg) => { cfg.Host(kafkaBootstrapServers); });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (isLocalEnvironment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IMongoDbInitializer>();
    initializer.InitializeAsync().Wait();
}

app.MapEventEndpoints();
app.Run();