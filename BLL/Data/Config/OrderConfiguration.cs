using System;
using BLL.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLL.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
            });

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                    );

            builder.Property(s => s.PaymentMethod)
                .HasConversion(
                    o => o.ToString(),
                    o => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), o)
                    );
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
