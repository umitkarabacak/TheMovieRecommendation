using MediatR;

namespace Application.Movies.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery : IRequest<MovieDetailDto>
    {
        public int MovieId { get; init; }

        public GetMovieDetailQuery(int movieId)
        {
            MovieId = movieId;
        }
    }
}
