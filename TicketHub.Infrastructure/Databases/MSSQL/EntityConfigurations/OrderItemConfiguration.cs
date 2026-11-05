using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketHub.Domain.Entities;

namespace TicketHub.Infrastructure.Databases.MSSQL.EntityConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.OrderId)
                .IsRequired();
            builder.Property(oi => oi.SeatId)
                .IsRequired();
            builder.Property(oi => oi.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.Seat)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(oi => oi.SeatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
