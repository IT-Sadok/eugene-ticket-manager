using TicketService.Api.Controllers;
using TicketService.Application.Extensions;
using TicketService.Infrastructure.Database;
using TicketService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var isLocalEnvironment = builder.Environment.EnvironmentName == "local";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

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
    scope.ServiceProvider.GetRequiredService<TicketServiceDbContext>().Database.EnsureCreated();
}
app.Run();
