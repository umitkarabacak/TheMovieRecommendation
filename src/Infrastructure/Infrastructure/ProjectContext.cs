using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProjectContext : DbContext, IProjectContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<MovieVote> MovieVotes { get; set; }

        public DbSet<User> User { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
