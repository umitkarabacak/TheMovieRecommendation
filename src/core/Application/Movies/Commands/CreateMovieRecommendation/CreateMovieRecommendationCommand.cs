using MediatR;

namespace Application.Movies.Commands.CreateMovieRecommendation
{
    public class CreateMovieRecommendationCommand : IRequest
    {
        public int MovieId { get; set; }

        public string EmailAddress { get; set; }
    }
}
