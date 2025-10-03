using MetricsApi.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.Infrastructure.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id).ValueGeneratedNever();
            builder.Property(oi => oi.ProductId).IsRequired();
            builder.Property(oi => oi.Sku).IsRequired().HasMaxLength(50);
            builder.Property(oi => oi.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(oi => oi.Quantity).IsRequired();
            builder.Property(oi => oi.CreatedAt).IsRequired();
            builder.Property(oi => oi.UpdatedAt).IsRequired();
        }
    }
}
