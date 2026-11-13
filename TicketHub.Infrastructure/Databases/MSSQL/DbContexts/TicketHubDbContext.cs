using Microsoft.EntityFrameworkCore;
using TicketHub.Application.Abstractions;
using TicketHub.Domain.Entities;

namespace TicketHub.Infrastructure.Databases.MSSQL.DbContexts
{
    public class TicketHubDbContext : DbContext, IUnitOfWork
    {
        public TicketHubDbContext(DbContextOptions<TicketHubDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketHubDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                                                            .Where(e => typeof(AuditableEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property(nameof(AuditableEntity.CreatedBy)).HasMaxLength(50);
                modelBuilder.Entity(entityType.ClrType).Property(nameof(AuditableEntity.UpdatedBy)).HasMaxLength(50);
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events => Set<Event>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Seat> Seats => Set<Seat>();
    }
}
