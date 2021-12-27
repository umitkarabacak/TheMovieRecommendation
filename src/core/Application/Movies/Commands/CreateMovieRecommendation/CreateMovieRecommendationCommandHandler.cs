using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Movies.Commands.CreateMovieRecommendation
{
    public class CreateMovieRecommendationCommandHandler : IRequestHandler<CreateMovieRecommendationCommand>
    {
        private readonly IProjectContext _projectContext;

        public CreateMovieRecommendationCommandHandler(IProjectContext projectContext)
        {
            _projectContext = projectContext;
        }

        public async Task<Unit> Handle(CreateMovieRecommendationCommand request, CancellationToken cancellationToken)
        {
            var movie = await _projectContext.Movies
                .FirstOrDefaultAsync(m => m.id.Equals(request.MovieId)
                    , cancellationToken
                );

            if (movie is null)
                throw new NotFoundException($"Movie id is: {request.MovieId}, Not Found", null);

            // send email operation...

            return Unit.Value;
        }
    }
}
