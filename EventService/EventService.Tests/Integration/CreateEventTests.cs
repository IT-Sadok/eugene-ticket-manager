using System.Net;
using EventService.Tests.Models;
using FluentAssertions;

namespace EventService.Tests.Integration;

public class CreateEventTests : TestBase
{
    [Fact]
    public async Task CreateEvent_WithValidData_ShouldReturnCreated()
    {
        // Arrange
        var eventRequest = new CreateEventRequest
        {
            Title = "Test Event",
            Description = "Test Description",
            StartedAt = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var response = await CreateEventAsync(eventRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateEvent_ShouldBeAvailableInGetAllEvents()
    {
        // Arrange
        var eventRequest = new CreateEventRequest
        {
            Title = "Test Event for GetAll",
            Description = "Test Description GetAll",
            StartedAt = DateTime.UtcNow.AddDays(2)
        };

        // Act
        var createResponse = await CreateEventAsync(eventRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        // Wait briefly to ensure data is saved
        await Task.Delay(500);

        // Get all events
        var events = await GetAsync<List<EventResponse>>("/events");

        // Assert
        events.Should().NotBeNull();
        events.Should().Contain(e => e.Title == eventRequest.Title && 
                                    e.Description == eventRequest.Description);
    }
} 