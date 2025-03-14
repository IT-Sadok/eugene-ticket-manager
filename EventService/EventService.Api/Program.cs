using EventService.Api.Controllers;
using EventService.Domain.RepositoryContracts;
using EventService.Infrastructure.Extensions;
using TicketService.Extensions;

var builder = WebApplication.CreateBuilder(args);

var isLocalEnvironment = builder.Environment.EnvironmentName == "local";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationServices();
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