using Confluent.Kafka;
using TicketService.Api.Controllers;
using TicketService.Application.Extensions;
using TicketService.Infrastructure.Database;
using TicketService.Infrastructure.Extensions;
using MassTransit;
using TicketService.Api.Helpers;
using TicketService.Application.Consumers;
using TicketService.Domain.Messages;

var builder = WebApplication.CreateBuilder(args);

var isLocalEnvironment = builder.Environment.EnvironmentName == "local";

// Get Kafka configuration
var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();
    x.AddRider(rider =>
    {
        rider.AddConsumer<TicketAvailabilityRequestConsumer>(c =>
            c.Options<BatchOptions>(o =>
            {
                o.MessageLimit = 50;
                o.TimeLimit = TimeSpan.FromSeconds(5);
            }));

        rider.UsingKafka((context, cfg) =>
        {
            cfg.Host(kafkaBootstrapServers);

            cfg.TopicEndpoint<ReserveTicketRequest>(KafkaConstants.TicketReservationRequestTopic, KafkaConstants.TicketServiceGroup, e =>
            {

                e.CreateIfMissing(t =>
                {
                    t.NumPartitions = 1;
                    t.ReplicationFactor = 1;
                });

                e.ConfigureConsumer<TicketAvailabilityRequestConsumer>(context);

                e.AutoOffsetReset = AutoOffsetReset.Earliest;
            });
        });
    });
});

var app = builder.Build();

if (isLocalEnvironment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapTicketsEndpoint();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<TicketServiceDbContext>().Database.EnsureDeleted();
    scope.ServiceProvider.GetRequiredService<TicketServiceDbContext>().Database.EnsureCreated();
}
app.Run();