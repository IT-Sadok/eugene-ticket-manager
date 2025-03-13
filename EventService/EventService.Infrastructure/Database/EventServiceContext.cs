using EventService.Domain.RepositoryModels;
using Microsoft.EntityFrameworkCore;

namespace EventService.Infrastructure.Database;

public class EventServiceContext(DbContextOptions<EventServiceContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public DbSet<Event> Events { get; set; }
}