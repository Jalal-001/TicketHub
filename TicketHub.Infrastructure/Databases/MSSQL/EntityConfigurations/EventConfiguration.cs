using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketHub.Domain.Entities;

namespace TicketHub.Infrastructure.Databases.MSSQL.EntityConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(e => e.Venue)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(e => e.StartsAt)
                .IsRequired();

        }
    }
}
