using System;
using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLL.Data.Config
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(s => s.MessageType)
               .HasConversion(
                   o => o.ToString(),
                   o => (MessageType)Enum.Parse(typeof(MessageType), o)
                   );
        }
    }
}
