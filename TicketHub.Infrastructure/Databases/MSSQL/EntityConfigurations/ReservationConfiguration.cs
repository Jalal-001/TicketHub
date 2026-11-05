using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketHub.Domain.Entities;

namespace TicketHub.Infrastructure.Databases.MSSQL.EntityConfigurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.UserId)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.SeatId)
                .IsRequired();
            builder.Property(r => r.ExpiresAt)
                .IsRequired();
            builder.Property(r => r.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(25);

            builder.HasOne(r => r.Seat)
                .WithMany()
                .HasForeignKey(r => r.SeatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
