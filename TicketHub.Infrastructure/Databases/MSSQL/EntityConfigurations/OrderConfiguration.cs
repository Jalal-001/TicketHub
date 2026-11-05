using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketHub.Domain.Entities;

namespace TicketHub.Infrastructure.Databases.MSSQL.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);
            builder.Property(o => o.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(25);

        }
    }
}
