using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using Testcontainers.MongoDb;

namespace EventService.Tests.Integration;

public class TestBase : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer;
    public WebApplicationFactory<Program> Factory { get; private set; }
    public HttpClient Client { get; private set; }

    public TestBase()
    {
        _mongoDbContainer = new MongoDbBuilder()
            .WithImage("mongo:6.0")
            .WithUsername("admin")
            .WithPassword("Passw0rd!")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _mongoDbContainer.StartAsync();

        var mongoConnectionString = _mongoDbContainer.GetConnectionString();

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["ConnectionStrings:EventServiceDbConnection"] = mongoConnectionString,
                        ["DatabaseName"] = "EventStoreDb",
                    });
                });
            });

        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await Factory.DisposeAsync();
        await _mongoDbContainer.DisposeAsync();
    }

    protected async Task<HttpResponseMessage> CreateEventAsync(object eventData)
    {
        return await Client.PostAsJsonAsync("/events", eventData);
    }

    protected async Task<T> GetAsync<T>(string url)
    {
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
} 