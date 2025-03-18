using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Outbox;
using TicketService.Domain.RepositoryModels;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Infrastructure.Database;

public class TicketServiceDbContext(DbContextOptions<TicketServiceDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>().Property(x => x.Status)
            .HasConversion(x => x.ToString(), x => (TicketStatus)Enum.Parse(typeof(TicketStatus), x));

        modelBuilder.Entity<OutboxMessage>().Property(x => x.Data).HasColumnType("jsonb");
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
}