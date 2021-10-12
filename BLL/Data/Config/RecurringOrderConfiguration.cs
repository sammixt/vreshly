using System;
using BLL.Entities;
using BLL.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLL.Data.Config
{
    public class RecurringOrderConfiguration : IEntityTypeConfiguration<RecurringOrder>
    {
        public RecurringOrderConfiguration()
        {
        }

        
        public void Configure(EntityTypeBuilder<RecurringOrder> builder)
        {
            builder.Property(s => s.Frequency)
                .HasConversion(
                    o => o.ToString(),
                    o => (RecurringFrequency)Enum.Parse(typeof(RecurringFrequency), o)
                    );
        }
    }
}
