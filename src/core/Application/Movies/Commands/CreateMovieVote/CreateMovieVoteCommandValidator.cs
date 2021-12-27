using FluentValidation;

namespace Application.Movies.Commands.CreateMovieVote
{
    public class CreateMovieVoteCommandValidator : AbstractValidator<CreateMovieVoteCommand>
    {
        public CreateMovieVoteCommandValidator()
        {
            RuleFor(u => u.MovieId)
                .NotEmpty()
                .WithMessage("Movie Id is required field");

            RuleFor(u => u.Vote)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Vote Greater Than Or Equal To 0")
                .LessThanOrEqualTo(10)
                .WithMessage("Vote Less Than Or Equal To 10");

            RuleFor(mv => mv.VoteNote)
                .MaximumLength(200)
                .WithMessage("Note area max length is 200");
        }
    }
}
