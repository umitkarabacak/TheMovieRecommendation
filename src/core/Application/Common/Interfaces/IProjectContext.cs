using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProjectContext
    {
        DbSet<Genre> Genres { get; }
        DbSet<Movie> Movies { get; }
        DbSet<MovieGenre> MovieGenres { get; }
        DbSet<MovieVote> MovieVotes { get; }
        DbSet<User> User { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
