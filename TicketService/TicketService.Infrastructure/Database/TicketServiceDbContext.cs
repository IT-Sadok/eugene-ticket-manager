using Microsoft.EntityFrameworkCore;
using TicketService.Domain.RepositoryModels;
using TicketService.Domain.RepositoryModels.Enums;

namespace TicketService.Infrastructure.Database;

public class TicketServiceDbContext(DbContextOptions<TicketServiceDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>().Property(x => x.Status)
            .HasConversion(x => x.ToString(), x => (TicketStatus)Enum.Parse(typeof(TicketStatus), x));
    }

    public DbSet<Ticket> Tickets { get; set; }
}