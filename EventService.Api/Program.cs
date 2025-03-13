using System.Reflection;
using EventService.Infrastructure.Database;
using EventService.Infrastructure.Extensions;
using TicketService;

var builder = WebApplication.CreateBuilder(args);

var isLocalEnvironment = builder.Environment.EnvironmentName == "local";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(),
        typeof(ApplicationAssemblyMarker).Assembly);
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
    scope.ServiceProvider.GetRequiredService<EventServiceContext>().Database.EnsureDeleted();
    scope.ServiceProvider.GetRequiredService<EventServiceContext>().Database.EnsureCreated();
}

app.Run();