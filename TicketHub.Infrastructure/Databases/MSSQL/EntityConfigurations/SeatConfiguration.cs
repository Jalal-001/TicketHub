using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketHub.Domain.Entities;

namespace TicketHub.Infrastructure.Databases.MSSQL.EntityConfigurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.EventId)
                .IsRequired();
            builder.Property(s => s.Row)
                .IsRequired();
            builder.Property(s => s.Number)
                .IsRequired();
            builder.Property(s => s.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(25);

            builder.HasOne(s => s.Event)
                .WithMany(e => e.Seats)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(s => new { s.EventId, s.Row, s.Number })
                .IsUnique();

            builder.HasIndex(s => new { s.EventId, s.Status });
        }
    }
}
