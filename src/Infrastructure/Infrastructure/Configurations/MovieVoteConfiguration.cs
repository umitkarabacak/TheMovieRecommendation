using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MovieVoteConfiguration : IEntityTypeConfiguration<MovieVote>
    {
        public void Configure(EntityTypeBuilder<MovieVote> builder)
        {
            builder.HasKey(g => new
            {
                g.MovieId,
                g.UserId
            });
        }
    }
}
