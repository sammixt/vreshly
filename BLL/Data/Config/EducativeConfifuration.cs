using System;
using BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BLL.Data.Config
{
    public class EducativeConfifuration : IEntityTypeConfiguration<Educative>
    {
        public EducativeConfifuration()
        {
        }

        public void Configure(EntityTypeBuilder<Educative> builder)
        {
            builder.Property(s => s.EducativeType)
               .HasConversion(
                   o => o.ToString(),
                   o => (EducativeType)Enum.Parse(typeof(EducativeType), o)
                   );
        }
    }
}
