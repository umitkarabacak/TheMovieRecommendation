using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryHandler : IRequestHandler<GetMovieDetailQuery, MovieDetailDto>
    {
        private readonly IProjectContext _projectContext;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryHandler(IProjectContext projectContext
            , IMapper mapper)
        {
            _projectContext = projectContext;
            _mapper = mapper;
        }

        public async Task<MovieDetailDto> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
        {
            var movie = await _projectContext.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieVotes)
                    .ThenInclude(mv => mv.User)
                .FirstOrDefaultAsync(m => m.id.Equals(request.MovieId));

            if (movie is null)
                throw new NotFoundException($"MovieId {request.MovieId}", movie);

            var movieDetail = _mapper.Map<MovieDetailDto>(movie);

            return movieDetail;
        }
    }
}
