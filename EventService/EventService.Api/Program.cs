using Confluent.Kafka;
using EventService.Api.Controllers;
using EventService.Api.Helpers;
using EventService.Application.Consumers;
using EventService.Application.Extensions;
using EventService.Domain.Messages;
using EventService.Domain.RepositoryContracts;
using EventService.Infrastructure.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var isLocalEnvironment = builder.Environment.EnvironmentName == "local";
var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS");

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
        rider.AddConsumer<OrderUpdateEventConsumer>(c => c.Options<BatchOptions>(o =>
        {
            o.MessageLimit = 50;
            o.TimeLimit = TimeSpan.FromSeconds(5);
        }));
        rider.AddProducer<ReserveTicketEvent>(KafkaConstants.TicketReservationRequestTopic);

        rider.UsingKafka((context, cfg) =>
        {
            cfg.Host(kafkaBootstrapServers);
            cfg.TopicEndpoint<OrderUpdateEvent>(KafkaConstants.OrderUpdateEventTopic, KafkaConstants.EventServiceGroup,
                e =>
                {
                    e.CreateIfMissing(t =>
                    {
                        t.NumPartitions = 1;
                        t.ReplicationFactor = 1;
                    });

                    e.ConfigureConsumer<OrderUpdateEventConsumer>(context);

                    e.AutoOffsetReset = AutoOffsetReset.Earliest;
                });
        });
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
    options.InstanceName = "EventsPlatform";
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
app.MapOrdersEndpoints();
app.Run();