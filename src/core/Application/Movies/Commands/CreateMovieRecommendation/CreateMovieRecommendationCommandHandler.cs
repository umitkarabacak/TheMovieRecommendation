using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.CreateMovieRecommendation
{
    public class CreateMovieRecommendationCommandHandler : IRequestHandler<CreateMovieRecommendationCommand>
    {
        private readonly IProjectContext _projectContext;
        private readonly ILogger<CreateMovieRecommendationCommandHandler> _logger;

        public CreateMovieRecommendationCommandHandler(IProjectContext projectContext
            , ILogger<CreateMovieRecommendationCommandHandler> logger)
        {
            _projectContext = projectContext;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateMovieRecommendationCommand request, CancellationToken cancellationToken)
        {
            var movie = await _projectContext.Movies
                .FirstOrDefaultAsync(m => m.id.Equals(request.MovieId)
                    , cancellationToken);

            if (movie is null)
                throw new NotFoundException($"Movie id is: {request.MovieId}, Not Found", null);

            // send email operation...
            _logger.LogInformation($"{request.EmailAddress} Send {movie.title} with {movie.overview}");

            return Unit.Value;
        }
    }
}
