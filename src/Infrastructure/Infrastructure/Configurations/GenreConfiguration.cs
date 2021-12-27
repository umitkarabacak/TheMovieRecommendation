using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(g => g.Id)
                .ValueGeneratedNever();

            builder
                .Property(g => g.Name)      // select configuration property
                .IsRequired()               // selected property is required
                .HasMaxLength(250)          // selected property max length is `250` chars.
                ;
        }
    }
}
