using FluentValidation;

namespace Application.Movies.Queries.GetMoviesListWithPagination
{
    public class GetMoviesListQueryValidator : AbstractValidator<GetMoviesListQuery>
    {
        private static readonly int MIN_VALUE = 0;
        private static readonly int MAX_PAGE_SIZE = 100;

        public GetMoviesListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(MIN_VALUE)
                .WithMessage($"Page index min value greather than {MIN_VALUE}");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(MIN_VALUE)
                .WithMessage($"Page size min value greather than {MIN_VALUE}")
                .LessThanOrEqualTo(MAX_PAGE_SIZE)
                .WithMessage($"Page size maxvalue less than {MAX_PAGE_SIZE}");
        }
    }
}
