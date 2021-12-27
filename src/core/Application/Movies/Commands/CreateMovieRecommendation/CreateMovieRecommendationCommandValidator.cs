using FluentValidation;

namespace Application.Movies.Commands.CreateMovieRecommendation
{
    public class CreateMovieRecommendationCommandValidator : AbstractValidator<CreateMovieRecommendationCommand>
    {
        public CreateMovieRecommendationCommandValidator()
        {
            RuleFor(u => u.MovieId)
               .NotEmpty()
               .WithMessage("Movie Id is required field");

            RuleFor(u => u.EmailAddress)
               .NotEmpty()
               .WithMessage("Email Address is required field")
               .EmailAddress()
               .WithMessage("Enter an email address!");
        }
    }
}
